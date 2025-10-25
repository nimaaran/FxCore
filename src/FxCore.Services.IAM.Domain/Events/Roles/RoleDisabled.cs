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
/// Defines a domain event model for when a role is disabled.
/// </summary>
public sealed record RoleDisabled : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDisabled"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="RoleKey"/>.</param>
    public RoleDisabled(IEventDependenciesProvider dependencies, RoleKey roleKey)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
    }

    /// <summary>
    /// Gets the relevant role's aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
