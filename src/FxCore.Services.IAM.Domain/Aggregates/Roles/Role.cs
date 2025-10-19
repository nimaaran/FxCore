// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

/// <summary>
/// Defines an aggregate root for the roles aggregate.
/// </summary>
public sealed class Role : EventDrivenRootBase<long, RoleKey>
{
    private Role()
        : base(
            id: default,
            key: new RoleKey(string.Empty),
            removed: false,
            @lock: AggregateLock.Empty)
    {
    }

    private Role(RoleKey roleKey, AggregateLock @lock)
        : base(
            id: default,
            key: roleKey,
            removed: false,
            @lock: @lock)
    {
    }

    private Role(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator roleKeyGenerator,
        string title,
        bool isSensitive,
        RoleTypes type,
        out Result result)
        : this(
            roleKey: roleKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dependencies.DateTimeService.Now()))
    {
        var @event = new RoleDefined(
            dependencies: dependencies,
            roleKey: this.Key,
            title: title,
            isSensitive: isSensitive,
            type: type,
            state: RoleStates.ENABLED);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Gets the role title.
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether the role is sensitive or not. For example we can enforce
    /// two-factor authentication for using sensitive roles.
    /// </summary>
    public bool IsSensitive { get; private set; } = false;

    /// <summary>
    /// Gets the role type.
    /// </summary>
    public RoleTypes Type { get; private set; } = RoleTypes.USER_DEFINED;

    /// <summary>
    /// Gets the role state.
    /// </summary>
    public RoleStates State { get; private set; } = RoleStates.DISABLED;

    /// <summary>
    /// Defines a new role.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="roleKeyGenerator">A role key generator service.</param>
    /// <param name="roleTitle">The role title.</param>
    /// <param name="isSensitive">
    ///     A value indicating whether the role is sensitive or not.
    /// </param>
    /// <param name="roleType">The role type.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Define(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator roleKeyGenerator,
        string roleTitle,
        bool isSensitive,
        RoleTypes roleType)
    {
        if (dependencies is null ||
            roleKeyGenerator is null ||
            string.IsNullOrWhiteSpace(roleTitle))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new Role(
                dependencies,
                roleKeyGenerator,
                roleTitle,
                isSensitive,
                roleType,
                out Result result);

        return result;
    }

    /// <summary>
    /// Enables the role.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Enable(IEventDependenciesProvider dependencies)
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
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Disable(IEventDependenciesProvider dependencies)
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
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Remove(IEventDependenciesProvider dependencies)
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

    /// <summary>
    /// Sets the <see cref="IsSensitive"/> flag.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result SetSensitivity(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new SensitivityFlagSet(
            dependencies: dependencies,
            roleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// unsets the <see cref="IsSensitive"/> flag.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result UnsetSensitivity(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new SensitivityFlagUnset(
            dependencies: dependencies,
            roleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <inheritdoc/>
    protected override Result DispatchEvent(IDomainEventModel @event)
    {
        var result = @event switch
        {
            RoleDefined e => this.OnRoleDefined(e),
            RoleEnabled _ => this.OnRoleEnabled(),
            RoleRemoved _ => this.OnRoleRemoved(),
            RoleDisabled _ => this.OnRoleDisabled(),
            SensitivityFlagSet _ => this.OnSensitivityFlagSet(),
            SensitivityFlagUnset _ => this.OnSensitivityFlagUnset(),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnRoleDefined(RoleDefined @event)
    {
        this.Title = @event.Title;
        this.IsSensitive = @event.IsSensitive;
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
        else if (this.Type == RoleTypes.SYSTEM_DEFINED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY, "System Roles should not be disabled.");
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
        else if (this.Type == RoleTypes.SYSTEM_DEFINED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY, "System Roles should not be removed.");
        }

        return this.Remove();
    }

    private Result OnSensitivityFlagSet()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.IsSensitive)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.IsSensitive = true;

        return Result.Completed();
    }

    private Result OnSensitivityFlagUnset()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.IsSensitive)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.IsSensitive = false;

        return Result.Completed();
    }
}
