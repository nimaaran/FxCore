// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Events.Passports;

/// <summary>
/// Defines a domain event for when a passport secret is changed.
/// </summary>
public sealed record class PassportSecretSet : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassportSecretSet"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="passportKey">The relevant passport key.</param>
    /// <param name="secretType">The relevant passport secret type.</param>
    /// <param name="encodedSecret">The new secret data that has been encoded.</param>
    /// <param name="expireDate">The secret expire date.</param>
    public PassportSecretSet(
        IEventDependenciesProvider dependencies,
        PassportKey passportKey,
        SecretTypes secretType,
        string encodedSecret,
        DateTimeOffset expireDate)
        : base(dependencies)
    {
        this.PassportKey = passportKey;
        this.SecretType = secretType;
        this.EncodedSecret = encodedSecret;
        this.ExpireDate = expireDate;
    }

    /// <summary>
    /// Gets the relevant passport aggregate key.
    /// </summary>
    public PassportKey PassportKey { get; }

    /// <summary>
    /// Gets the relevant passport secret type.
    /// </summary>
    public SecretTypes SecretType { get; }

    /// <summary>
    /// Gets the encoded secret data.
    /// </summary>
    public string EncodedSecret { get; }

    /// <summary>
    /// Gets the secret expire date.
    /// </summary>
    public DateTimeOffset ExpireDate { get; }
}
