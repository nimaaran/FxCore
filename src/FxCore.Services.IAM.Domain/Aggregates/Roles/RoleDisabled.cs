// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

/// <summary>
/// Defines a domain event model for when a role is disabled.
/// </summary>
public sealed record RoleDisabled : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDisabled"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="roleKey">The relevant role aggregate key.</param>
    public RoleDisabled(IEventDependenciesProvider dependencies, RoleKey roleKey)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
    }

    /// <summary>
    /// Gets the relevant role aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
