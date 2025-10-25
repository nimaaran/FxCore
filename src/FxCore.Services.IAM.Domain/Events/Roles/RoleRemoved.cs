// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Events.Roles;

/// <summary>
/// Defines a domain event model for when a role is removed.
/// </summary>
public sealed record RoleRemoved : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleRemoved"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="RoleKey"/>.</param>
    public RoleRemoved(IEventDependenciesProvider dependencies, RoleKey roleKey)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
    }

    /// <summary>
    /// Gets the relevant role's aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
