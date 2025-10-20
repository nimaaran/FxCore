// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Events.Passports;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines the base class for different types of passports.
/// </summary>
/// <typeparam name="TAggregateRootModel">Type of the concrete passport aggregate root.</typeparam>
public abstract class Passport<TAggregateRootModel> : EventDrivenRootBase<long, PassportKey>
    where TAggregateRootModel : IAggregateRootModel
{
    private readonly List<Secret> secrets = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Passport{TAggregateRootModel}"/> class.
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
    /// Initializes a new instance of the <see cref="Passport{TAggregateRootModel}"/> class.
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
        IPassportKeyGenerator<TAggregateRootModel> passportKeyGenerator,
        AccountKey accountKey,
        string identity,
        PassportTypes type,
        PassportStates state,
        out Result result)
        : this(
            identityKey: passportKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dependencies.DateTimeService.Now()))
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
    public IReadOnlyCollection<Secret> Secrets => this.secrets.AsReadOnly();

    /// <summary>
    /// Authenticates the passport by using the provided secret.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="secretEncoder">A passport secret encoder.</param>
    /// <param name="secret">A secret value that should be used to authenticate user.</param>
    /// <param name="secretType">The secret type.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Verify(
        IEventDependenciesProvider dependencies,
        ISecretEncoder secretEncoder,
        string secret,
        SecretTypes secretType)
    {
        Result result;

        var secretMatched = false;
        var encodedSecret = secretEncoder.Encode(secret);

        foreach (var item in this.secrets)
        {
            var evaluationResult = item.Evaluate(
                dependencies.DateTimeService,
                encodedSecret);

            if (evaluationResult.State == ResultStates.COMPLETED)
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
                secretType: secretType);

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

    /// <summary>
    /// Changes the passport secret to a new value.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="configs">An object that provides authentication configurations.</param>
    /// <param name="secretEncoder">A passport secret encrypter.</param>
    /// <param name="newSecret">A new secret that should be used to authenticate user.</param>
    /// <param name="secretType">The new secret type.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result SetSecret(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfigProvider configs,
        ISecretEncoder secretEncoder,
        string newSecret,
        SecretTypes secretType)
    {
        var encodedSecret = secretEncoder.Encode(newSecret);
        var lifetime = secretType == SecretTypes.PASSWORD ? configs.PasswordLifetime :
                                                            configs.PasscodeLifetime;

        var @event = new PassportSecretSet(
            dependencies: dependencies,
            passportKey: this.Key,
            secretType: secretType,
            encodedSecret: encodedSecret,
            expireDate: dependencies.DateTimeService.Now().Add(lifetime));

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <inheritdoc/>
    protected override Result DispatchEvent(IDomainEventModel @event)
    {
        var result = @event switch
        {
            PassportIssued e => this.OnPassportIssued(e),
            PassportVerificationFailed _ => this.OnPassportVerificationFailed(),
            PassportVerified e => this.OnPassportVerified(e),
            PassportSecretSet e => this.OnPassportSecretSet(e),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnPassportVerificationFailed()
    {
        return Result.Terminated(ResultCodes.AUTHENTICATION_REQUIRED, "Passport verification failed.");
    }

    private Result OnPassportVerified(PassportVerified @event)
    {
        this.RemoveSecret(@event.SecretType);

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
        var secretResult = Secret.Create(
            secretType: @event.SecretType,
            encodedSecret: @event.EncodedSecret,
            expireDate: @event.ExpireDate);

        if (secretResult.State == ResultStates.COMPLETED &&
            secretResult.TryGetOutcome<Secret>(out var outcome))
        {
            this.RemoveSecret(@event.SecretType);

            this.secrets.Add(outcome!);
            return Result.Completed();
        }

        return Result.Terminated(ResultCodes.INCONSISTENCY, "Secret setting not completed.");
    }

    private Result RemoveSecret(SecretTypes secretType)
    {
        var secret = this.secrets.FirstOrDefault(s => s.Type == secretType);

        if (secret is not null)
        {
            var removeResult = secret.Remove();
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
}
