// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines a class for any kind of passport secrets.
/// </summary>
public class Secret : EntityBase<long>
{
    private Secret()
        : base(id: default, removed: false)
    {
    }

    private Secret(string encodedValue, DateTimeOffset expireDate, SecretTypes type)
        : this()
    {
        this.EncodedValue = encodedValue;
        this.ExpireDate = expireDate;
        this.Type = type;
    }

    /// <summary>
    /// Gets an encoded secret value.
    /// </summary>
    public string EncodedValue { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the expire date of the secret data.
    /// </summary>
    public DateTimeOffset ExpireDate { get; private set; } = DateTimeOffset.MinValue;

    /// <summary>
    /// Gets the secret type.
    /// </summary>
    public SecretTypes Type { get; private set; } = SecretTypes.PASSWORD;

    internal static Result Create(SecretTypes secretType, string encodedSecret, DateTimeOffset expireDate)
    {
        var newSecret = new Secret(
            encodedValue: encodedSecret,
            expireDate: expireDate,
            type: secretType);

        return Result.Completed(newSecret);
    }

    internal new Result Remove()
    {
        return base.Remove();
    }

    internal Result Evaluate(
        IDateTimeService dateTimeService,
        string encodedSecret)
    {
        var matched = encodedSecret == this.EncodedValue;
        var expired = this.ExpireDate >= dateTimeService.Now();

        if (matched && !expired && !this.Removed)
        {
            return Result.Completed();
        }

        return Result.Terminated(
            code: ResultCodes.AUTHENTICATION_REQUIRED,
            message: "The secret is not matched or expired.");
    }
}
