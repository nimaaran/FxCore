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
/// Defines a domain event model that is raised when a new role is defined in the system.
/// </summary>

public sealed record RoleDefined : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDefined"/> class.
    /// </summary>
    /// <param name="dependencies">Domain event dependencies provider.</param>
    /// <param name="roleKey">The relevant role aggregate key.</param>
    /// <param name="title">The relevant role title.</param>
    /// <param name="isSensitive">
    /// A value indicating whether the role is marked as sensitive.
    /// </param>
    /// <param name="type">The relevant role type.</param>
    /// <param name="state">The relevant role state.</param>
    public RoleDefined(
        IEventDependenciesProvider dependencies,
        RoleKey roleKey,
        string title,
        bool isSensitive,
        RoleTypes type,
        RoleStates state)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
        this.Title = title;
        this.IsSensitive = isSensitive;
        this.Type = type;
        this.State = state;
    }

    /// <summary>
    /// Gets the relevant role aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }

    /// <summary>
    /// Gets the relevant role title.
    /// </summary>
    public string Title { get; internal set; }

    /// <summary>
    /// Gets a value indicating whether the role is marked as sensitive or not.
    /// </summary>
    public bool IsSensitive { get; internal set; }

    /// <summary>
    /// Gets the relevant role type.
    /// </summary>
    public RoleTypes Type { get; internal set; }

    /// <summary>
    /// Gets the relevant role state.
    /// </summary>
    public RoleStates State { get; internal set; }
}
