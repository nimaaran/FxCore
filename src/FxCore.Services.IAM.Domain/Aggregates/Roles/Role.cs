// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates;
using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Events.Roles;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

/// <summary>
/// Defines an aggregate root for the roles aggregate.
/// </summary>
/// <typeparam name="TRole">Type of the role.</typeparam>
public abstract class Role<TRole> : EventDrivenRootBase<long, RoleKey>
    where TRole : Role<TRole>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Role{TRole}"/> class.
    /// </summary>
    protected Role()
        : base(
            id: default,
            key: new RoleKey(string.Empty),
            removed: false,
            @lock: AggregateLock.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Role{TRole}"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKeyGenerator">A role key generator.</param>
    /// <param name="title">See <see cref="Title"/>.</param>
    /// <param name="type">See <see cref="Type"/>.</param>
    /// <param name="state">See <see cref="State"/>.</param>
    /// <param name="result">See <see cref="Result"/>.</param>
    protected Role(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator<TRole> roleKeyGenerator,
        string title,
        RoleTypes type,
        RoleStates state,
        out Result result)
        : this(
            roleKey: roleKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dependencies.DateTimeService.UtcNow()))
    {
        var @event = new RoleDefined(
            dependencies: dependencies,
            roleKey: this.Key,
            title: title,
            type: type,
            state: state);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    private Role(RoleKey roleKey, AggregateLock @lock)
    : base(
        id: default,
        key: roleKey,
        removed: false,
        @lock: @lock)
    {
    }

    /// <summary>
    /// Gets the role title.
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets the role type.
    /// </summary>
    public RoleTypes Type { get; private set; } = RoleTypes.USER_DEFINED;

    /// <summary>
    /// Gets the role state.
    /// </summary>
    public RoleStates State { get; private set; } = RoleStates.DISABLED;

    /// <summary>
    /// Enables the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Enable(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleEnabled(
            dependencies: dependencies,
            roleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Disables the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Disable(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleDisabled(
            dependencies: dependencies,
            roleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Removes the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Remove(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleRemoved(
            dependencies: dependencies,
            roleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <inheritdoc/>
    protected override Result DispatchEvent(IDomainEvent @event)
    {
        var result = @event switch
        {
            RoleDefined e => this.OnRoleDefined(e),
            RoleEnabled _ => this.OnRoleEnabled(),
            RoleRemoved _ => this.OnRoleRemoved(),
            RoleDisabled _ => this.OnRoleDisabled(),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnRoleDefined(RoleDefined @event)
    {
        this.Title = @event.Title;
        this.Type = @event.Type;
        this.State = @event.State;

        return Result.Completed(this.Key);
    }

    private Result OnRoleEnabled()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State == RoleStates.ENABLED)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.State = RoleStates.ENABLED;

        return Result.Completed();
    }

    private Result OnRoleDisabled()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State == RoleStates.DISABLED)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.State = RoleStates.DISABLED;

        return Result.Completed();
    }

    private Result OnRoleRemoved()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        return this.Remove();
    }
}
