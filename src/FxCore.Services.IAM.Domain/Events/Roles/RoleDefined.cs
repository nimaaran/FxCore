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
/// Defines the event that is raised when a role is defined.
/// </summary>

public sealed record RoleDefined : DomainEventBase
{
    /// <summary>
    /// Initializes a new instance of the <see cref="RoleDefined"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="RoleKey"/>.</param>
    /// <param name="title">See <see cref="Title"/>.</param>
    /// <param name="type">See <see cref="Type"/>.</param>
    /// <param name="state">See <see cref="State"/>.</param>
    public RoleDefined(
        IEventDependenciesProvider dependencies,
        RoleKey roleKey,
        string title,
        RoleTypes type,
        RoleStates state)
        : base(dependencies)
    {
        this.RoleKey = roleKey;
        this.Title = title;
        this.Type = type;
        this.State = state;
    }

    /// <summary>
    /// Gets the relevant role's aggregate key.
    /// </summary>
    public RoleKey RoleKey { get; }

    /// <summary>
    /// Gets the relevant role's title.
    /// </summary>
    public string Title { get; internal set; }

    /// <summary>
    /// Gets the relevant role's type.
    /// </summary>
    public RoleTypes Type { get; internal set; }

    /// <summary>
    /// Gets the relevant role's state.
    /// </summary>
    public RoleStates State { get; internal set; }
}
