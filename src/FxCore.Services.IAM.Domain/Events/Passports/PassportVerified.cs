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
/// Defines a domain event for when an account verified by a passport.
/// </summary>
public sealed record class PassportVerified : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassportVerified"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="passportKey">The relevant passport key.</param>
    /// <param name="passportType">The relevant passport type.</param>
    /// <param name="secretType">The relevant passport secret type.</param>
    public PassportVerified(
        IEventDependenciesProvider dependencies,
        PassportKey passportKey,
        PassportTypes passportType,
        SecretTypes secretType)
        : base(dependencies)
    {
        this.PassportKey = passportKey;
        this.PassportType = passportType;
        this.SecretType = secretType;
    }

    /// <summary>
    /// Gets the relevant passport aggregate key.
    /// </summary>
    public PassportKey PassportKey { get; }

    /// <summary>
    /// Gets the relevant passport type.
    /// </summary>
    public PassportTypes PassportType { get; }

    /// <summary>
    /// Gets the relevant passport secret type.
    /// </summary>
    public SecretTypes SecretType { get; }
}
