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
/// Defines a domain event for when an account verification is failed.
/// </summary>
public sealed record class PassportVerificationFailed : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="PassportVerificationFailed"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="passportKey">The relevant passport key.</param>
    /// <param name="passportType">The relevant passport type.</param>
    public PassportVerificationFailed(
        IEventDependenciesProvider dependencies,
        PassportKey passportKey,
        PassportTypes passportType)
        : base(dependencies)
    {
        this.PassportKey = passportKey;
        this.PassportType = passportType;
    }

    /// <summary>
    /// Gets the relevant passport aggregate key.
    /// </summary>
    public PassportKey PassportKey { get; }

    /// <summary>
    /// Gets the relevant passport type.
    /// </summary>
    public PassportTypes PassportType { get; }
}
