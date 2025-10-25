// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines the event that is raised when a role has revoked from an account.
/// </summary>
public sealed record RoleRevoked : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRevoked"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="accountKey">See <see cref="AccountKey"/>.</param>
    /// <param name="roleKey">See <see cref="RoleKey"/>.</param>
    public RoleRevoked(
        IEventDependenciesProvider dependencies,
        AccountKey accountKey,
        RoleKey roleKey)
        : base(dependencies)
    {
        this.AccountKey = accountKey;
        this.RoleKey = roleKey;
    }

    /// <summary>
    /// Gets the relevant account's aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }

    /// <summary>
    /// Gets the relevant role's aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
