// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates;
using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Events.Passports;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines the base class for different types of passports.
/// </summary>
/// <typeparam name="TPassport">Type of the passport.</typeparam>
public abstract class Passport<TPassport> : EventDrivenRootBase<long, PassportKey>
    where TPassport : Passport<TPassport>
{
    private readonly List<ISecret> secrets = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Passport{TPassport}"/> class.
    /// </summary>
    protected Passport()
        : base(
            id: default,
            key: new PassportKey(string.Empty),
            removed: false,
            @lock: AggregateLock.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Passport{TPassport}"/> class.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="passportKeyGenerator">A passport key generator.</param>
    /// <param name="accountKey">The relevant account key.</param>
    /// <param name="identity">The passport identity.</param>
    /// <param name="type">The passport type.</param>
    /// <param name="state">The passport state.</param>
    /// <param name="result">An object as type of the <see cref="Result"/>.</param>
    protected Passport(
        IEventDependenciesProvider dependencies,
        IPassportKeyGenerator<TPassport> passportKeyGenerator,
        AccountKey accountKey,
        string identity,
        PassportTypes type,
        PassportStates state,
        out Result result)
        : this(
            identityKey: passportKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dependencies.DateTimeService.UtcNow()))
    {
        var @event = new PassportIssued(
            dependencies: dependencies,
            passportKey: this.Key,
            accountKey: accountKey,
            identity: identity,
            type: type,
            state: state);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    private Passport(PassportKey identityKey, AggregateLock @lock)
       : base(
           id: default,
           key: identityKey,
           removed: false,
           @lock: @lock)
    {
    }

    /// <summary>
    /// Gets the relevant account key.
    /// </summary>
    public AccountKey AccountKey { get; private set; } = new(string.Empty);

    /// <summary>
    /// Gets the passport identity.
    /// </summary>
    public string Identity { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the passport type.
    /// </summary>
    public PassportTypes Type { get; private set; } = PassportTypes.BASIC;

    /// <summary>
    /// Gets the passport state.
    /// </summary>
    public PassportStates State { get; private set; } = PassportStates.DEACTIVATED;

    /// <summary>
    /// Gets the collection of secrets associated with the passport.
    /// </summary>
    public IReadOnlyCollection<ISecret> Secrets => this.secrets.AsReadOnly();

    /// <summary>
    /// Creates a new secret object by using provided secret data.
    /// </summary>
    /// <typeparam name="TSecret">Type of desired passport secret type.</typeparam>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="secretBuilder">A secret builder.</param>
    /// <param name="secret">A raw data that should be used for creting secret object.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result SetSecret<TSecret>(
        IEventDependenciesProvider dependencies,
        ISecretBuilder<TSecret> secretBuilder,
        string secret)
        where TSecret : Secret<TSecret>
    {
        if (dependencies is null ||
            secretBuilder is null ||
            string.IsNullOrEmpty(secret))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var secretObject = secretBuilder.Build(secret);

        if (!this.GetTSecretType<TSecret>(out SecretTypes? secretType))
        {
            return Result.Terminated(ResultCodes.CONFLICT);
        }

        var @event = new PassportSecretSet(
            dependencies: dependencies,
            passportKey: this.Key,
            secretType: secretType!.Value,
            expireDate: DateTimeOffset.Now,
            secret: secretObject);

        return this.ApplyEvent(@event, isNew: true);
    }

    /// <summary>
    /// Creates a new secret object by using a system generated secret data.
    /// </summary>
    /// <typeparam name="TSecret">Type of desired passport secret type.</typeparam>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="secretGenerator">A secret data generator.</param>
    /// <param name="secretBuilder">A secret builder.</param>
    /// <param name="secret">A raw data that should be used for creting secret object.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result GenerateSecret<TSecret>(
       IEventDependenciesProvider dependencies,
       ISecretGenerator<TSecret> secretGenerator,
       ISecretBuilder<TSecret> secretBuilder,
       out string secret)
       where TSecret : Secret<TSecret>
    {
        secret = string.Empty;
        if (dependencies is null ||
            secretGenerator is null ||
            secretBuilder is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        if (!this.GetTSecretType<TSecret>(out SecretTypes? secretType))
        {
            return Result.Terminated(ResultCodes.CONFLICT);
        }

        secret = secretGenerator.Generate();
        var generatedSecret = secretBuilder.Build(secret);

        var @event = new PassportSecretGenerated(
            dependencies: dependencies,
            passportKey: this.Key,
            secretType: secretType!.Value,
            expireDate: DateTimeOffset.Now,
            generatedSecret: generatedSecret);

        return this.ApplyEvent(@event, isNew: true);
    }

    /// <summary>
    /// Authenticates the passport by using the provided secret.
    /// </summary>
    /// <typeparam name="TSecret">Type of desired passport secret type.</typeparam>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="secretEvaluator">A passport secret evaluator.</param>
    /// <param name="secret">A secret value that should be used to authenticate user.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Verify<TSecret>(
        IEventDependenciesProvider dependencies,
        ISecretEvaluator<TSecret> secretEvaluator,
        string secret)
        where TSecret : Secret<TSecret>
    {
        Result result;

        var secretMatched = false;

        if (!this.GetTSecretType<TSecret>(out SecretTypes? secretType))
        {
            return Result.Terminated(ResultCodes.CONFLICT);
        }

        foreach (var item in this.secrets)
        {
            if (secretEvaluator.Evaluate(
                secret,
                item.EncodedValue,
                item.ExpireDate))
            {
                secretMatched = true;
                break;
            }
        }

        if (secretMatched)
        {
            var @event = new PassportVerified(
                dependencies: dependencies,
                passportKey: this.Key,
                passportType: this.Type,
                secretType: secretType!.Value);

            result = this.ApplyEvent(@event: @event, isNew: true);
        }
        else
        {
            var @event = new PassportVerificationFailed(
                dependencies: dependencies,
                passportKey: this.Key,
                passportType: this.Type);

            result = this.ApplyEvent(@event: @event, isNew: true);
        }

        return result;
    }

    /// <inheritdoc/>
    protected override Result DispatchEvent(IDomainEvent @event)
    {
        var result = @event switch
        {
            PassportIssued e => this.OnPassportIssued(e),
            PassportVerificationFailed _ => this.OnPassportVerificationFailed(),
            PassportVerified e => this.OnPassportVerified(e),
            PassportSecretSet e => this.OnPassportSecretSet(e),
            PassportSecretGenerated e => this.OnPassportSecretGenerated(e),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnPassportVerificationFailed()
    {
        return Result.Terminated(ResultCodes.UNAUTHORIZED, "Passport verification failed.");
    }

    private Result OnPassportVerified(PassportVerified @event)
    {
        if (@event.SecretType == SecretTypes.PASSCODE)
        {
            this.RemoveSecret<Passcode>(@event.SecretType);
        }

        return Result.Completed();
    }

    private Result OnPassportIssued(PassportIssued @event)
    {
        this.AccountKey = @event.AccountKey;
        this.Identity = @event.Identity;
        this.Type = @event.Type;
        this.State = @event.State;

        return Result.Completed(this.Key);
    }

    private Result OnPassportSecretSet(PassportSecretSet @event)
    {
        var secretResult = @event.Secret;

        if (@event.SecretType == SecretTypes.PASSWORD)
        {
            this.RemoveSecret<Password>(@event.SecretType);
        }
        else if (@event.SecretType == SecretTypes.PASSCODE)
        {
            this.RemoveSecret<Passcode>(@event.SecretType);
        }

        this.secrets.Add(secretResult);

        return Result.Completed();
    }

    private Result OnPassportSecretGenerated(PassportSecretGenerated @event)
    {
        var secretResult = @event.GeneratedSecret;

        if (@event.SecretType == SecretTypes.PASSWORD)
        {
            this.RemoveSecret<Password>(@event.SecretType);
        }
        else if (@event.SecretType == SecretTypes.PASSCODE)
        {
            this.RemoveSecret<Passcode>(@event.SecretType);
        }

        this.secrets.Add(secretResult);

        return Result.Completed();
    }

    private Result RemoveSecret<TSecret>(SecretTypes secretType)
        where TSecret : Secret<TSecret>
    {
        var secret = this.secrets.FirstOrDefault(s => s.Type == secretType);

        if (secret is not null)
        {
            var removeResult = ((Secret<TSecret>)secret).Remove();
            if (removeResult.State == ResultStates.COMPLETED)
            {
                return Result.Completed();
            }

            return removeResult;
        }

        return Result.Terminated(
            code: ResultCodes.NOT_FOUND,
            message: "The specified secret type was not found.");
    }

    private bool GetTSecretType<TSecret>(out SecretTypes? type)
        where TSecret : Secret<TSecret>
    {
        var secretType = typeof(TSecret);
        type = null;

        if (secretType == typeof(Password))
        {
            type = SecretTypes.PASSWORD;
            return true;
        }
        else if (secretType == typeof(Passcode))
        {
            type = SecretTypes.PASSCODE;
            return true;
        }

        return true;
    }
}
