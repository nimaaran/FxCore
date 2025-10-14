using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

public sealed class Role : EventDrivenRootBase<long, RoleKey>
{
    private Role() : base(
        id: default,
        key: new RoleKey(string.Empty),
        removed: false,
        @lock: AggregateLock.Empty)
    {
    }

    private Role(RoleKey roleKey, AggregateLock @lock) : base(
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
        bool twoFactorRequired,
        RoleTypes type,
        out Result result) : this(
            roleKey: roleKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dateTimeService.Now()))
    {
        var @event = new RoleDefined(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: this.Lock.Timestamp,
            RoleKey: this.Key,
            RoleTitle: title,
            TwoFactorRequired: twoFactorRequired,
            RoleType: type,
            RoleState: RoleStates.ENABLED);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    public string Title { get; private set; } = string.Empty;
    public bool TwoFactorRequired { get; private set; } = false;
    public RoleTypes Type { get; private set; } = RoleTypes.USER_DEFINED;
    public RoleStates State { get; private set; } = RoleStates.DISABLED;

    public static Result Define(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IRoleKeyGenerator aggregateKeyGenerator,
        string roleTitle,
        bool twoFactorRequired,
        RoleTypes roleType)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            aggregateKeyGenerator is null ||
            string.IsNullOrWhiteSpace(roleTitle))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new Role(
                dateTimeService,
                trackingKeyGenerator,
                aggregateKeyGenerator,
                roleTitle,
                twoFactorRequired,
                roleType,
                out Result result);

        return result;
    }

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
            RoleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    public Result Disable(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleDisabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            RoleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    public Result Remove(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleRemoved(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            RoleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    public Result SetTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorSet(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            RoleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    public Result UnsetTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorUnset(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            RoleKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected override Result DispatchEvent(IDomainEventModel @event)
    {
        var result = @event switch
        {
            RoleDefined e => this.OnRoleDefined(e),
            RoleEnabled e => this.OnRoleEnabled(e),
            RoleRemoved e => this.OnRoleRemoved(e),
            RoleDisabled e => this.OnRoleDisabled(e),
            TwoFactorSet e => this.OnTwoFactorSet(e),
            TwoFactorUnset e => this.OnTwoFactorUnset(e),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnRoleDefined(RoleDefined @event)
    {
        this.Title = @event.RoleTitle;
        this.TwoFactorRequired = @event.TwoFactorRequired;
        this.Type = @event.RoleType;
        this.State = @event.RoleState;

        return Result.Completed(this.Key);
    }

    private Result OnRoleEnabled(RoleEnabled _)
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

    private Result OnRoleDisabled(RoleDisabled _)
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

    private Result OnRoleRemoved(RoleRemoved _)
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

    private Result OnTwoFactorSet(TwoFactorSet _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.TwoFactorRequired)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.TwoFactorRequired = true;

        return Result.Completed();
    }

    private Result OnTwoFactorUnset(TwoFactorUnset _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.TwoFactorRequired)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.TwoFactorRequired = false;

        return Result.Completed();
    }
}
