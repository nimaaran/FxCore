// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Events.Accounts;

/// <summary>
/// Defines an event that is triggered when a role is revoked from an account.
/// </summary>
public sealed record RoleRevoked : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRevoked"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="accountKey">The relevant account aggregate key.</param>
    /// <param name="roleKey">The relevant role aggregate key.</param>
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
    /// Gets the relevant account aggregate key.
    /// </summary>
    public AccountKey AccountKey { get; }

    /// <summary>
    /// Gets the relevant role aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
