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
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IRoleKeyGenerator roleKeyGenerator,
        string title,
        bool isSensitive,
        RoleTypes type,
        out Result result)
        : this(
            roleKey: roleKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dateTimeService.Now()))
    {
        var @event = new RoleDefined(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: this.Lock.Timestamp,
            Key: this.Key,
            Title: title,
            IsSensitive: isSensitive,
            Type: type,
            State: RoleStates.ENABLED);

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
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <param name="roleKeyGenerator">A role key generator service.</param>
    /// <param name="roleTitle">The role title.</param>
    /// <param name="isSensitive">
    ///     A value indicating whether the role is sensitive or not.
    /// </param>
    /// <param name="roleType">The role type.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Define(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IRoleKeyGenerator roleKeyGenerator,
        string roleTitle,
        bool isSensitive,
        RoleTypes roleType)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            roleKeyGenerator is null ||
            string.IsNullOrWhiteSpace(roleTitle))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new Role(
                dateTimeService,
                trackingKeyGenerator,
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
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Enable(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleEnabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Disables the role.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Disable(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleDisabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Removes the role.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result Remove(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleRemoved(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Sets the <see cref="IsSensitive"/> flag.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result SetSensitivity(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new SensitivityFlagSet(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// unsets the <see cref="IsSensitive"/> flag.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator service provider.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public Result UnsetSensitivity(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new SensitivityFlagUnset(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

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
