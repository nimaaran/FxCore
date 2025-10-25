// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Events.Passports;

/// <summary>
/// Defines a domain event for when a passport secret is generated.
/// </summary>
public sealed record class PassportSecretGenerated : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassportSecretGenerated"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="passportKey">See <see cref="PassportKey"/>.</param>
    /// <param name="secretType">See <see cref="SecretType"/>.</param>
    /// <param name="expireDate">See <see cref="ExpireDate"/>.</param>
    /// <param name="generatedSecret">See <see cref="GeneratedSecret"/>.</param>
    public PassportSecretGenerated(
        IEventDependenciesProvider dependencies,
        PassportKey passportKey,
        SecretTypes secretType,
        DateTimeOffset expireDate,
        ISecret generatedSecret)
        : base(dependencies)
    {
        this.PassportKey = passportKey;
        this.SecretType = secretType;
        this.ExpireDate = expireDate;
        this.GeneratedSecret = generatedSecret;
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
    /// Gets the secret expire date.
    /// </summary>
    public DateTimeOffset ExpireDate { get; }

    /// <summary>
    /// Gets the passport secret data.
    /// </summary>
    internal ISecret GeneratedSecret { get; }
}
