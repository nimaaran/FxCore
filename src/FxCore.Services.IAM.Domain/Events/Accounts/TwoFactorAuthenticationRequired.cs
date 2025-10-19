// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines an event that is raised when an account is verified but two-factor authentication
/// is required.
/// </summary>
public record class TwoFactorAuthenticationRequired : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TwoFactorAuthenticationRequired"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="accountKey">The relevant account aggregate key.</param>
    public TwoFactorAuthenticationRequired(
        IEventDependenciesProvider dependencies,
        AccountKey accountKey)
        : base(dependencies)
    {
        this.AccountKey = accountKey;
    }

    /// <summary>
    /// Gets the relevant account aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }
}
