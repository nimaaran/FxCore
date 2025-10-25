// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines the event that is raised when the account' two-factor authentication has been
/// deactivated.
/// </summary>
public sealed record TwoFactorDisabled : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TwoFactorDisabled"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="accountKey">See <see cref="AccountKey"/>.</param>
    public TwoFactorDisabled(IEventDependenciesProvider dependencies, AccountKey accountKey)
        : base(dependencies)
    {
        this.AccountKey = accountKey;
    }

    /// <summary>
    /// Gets the relevant account's aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }
}
