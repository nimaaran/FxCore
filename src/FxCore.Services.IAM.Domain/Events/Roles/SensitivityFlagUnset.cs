// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Events.Roles;

/// <summary>
/// Defines a domain event model for when a sensitivity flag is unset on a role.
/// </summary>
public sealed record SensitivityFlagUnset : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SensitivityFlagUnset"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="roleKey">The relevant role aggregate key.</param>
    public SensitivityFlagUnset(IEventDependenciesProvider dependencies, RoleKey roleKey)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
    }

    /// <summary>
    /// Gets the relevant role aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }
}
