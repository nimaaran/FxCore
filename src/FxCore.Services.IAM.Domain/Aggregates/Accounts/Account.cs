using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public abstract class Account<TAggregateRootModel> : EventDrivenRootBase<long, AccountKey>
    where TAggregateRootModel : IAggregateRootModel
{
    private readonly List<AccountRole> assignedRoles = [];

    protected Account()
        : base(id: default, key: new AccountKey(string.Empty), removed: false, @lock: AggregateLock.Empty)
    {
    }

    private Account(AccountKey accountKey, AggregateLock @lock) : base(
       id: default,
       key: accountKey,
       removed: false,
       @lock: @lock)
    {
    }

    protected Account(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IAccountKeyGenerator<TAggregateRootModel> aggregateKeyGenerator,
        string displayName,
        AccountTypes accountType,
        AccountStates accountState,
        out Result result)
        : this(
            accountKey: aggregateKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dateTimeService.Now()))
    {
        var @event = new AccountRegistered(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: this.Lock.Timestamp,
            AccountKey: this.Key,
            DisplayName: displayName,
            AccountType: accountType,
            AccountState: accountState);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    public string DisplayName { get; private set; } = string.Empty;
    public bool TwoFactorEnabled { get; private set; } = false;
    public AccountTypes Type { get; private set; } = AccountTypes.GUEST;
    public AccountStates State { get; private set; } = AccountStates.REGISTERED;
    public IReadOnlyCollection<AccountRole> AssignedRoles => assignedRoles.AsReadOnly();

    protected Result Activate(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountActivated(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result Deactivate(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountDeactivated(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result Restrict(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountRestricted(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result Close(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountClosed(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result Ban(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountBanned(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result AssignRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey,
        bool twoFactorRequired)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            roleKey == new RoleKey(string.Empty))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleAssigned(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key,
            RoleKey: roleKey);

        if (!twoFactorRequired ||
            (this.EnableTwoFactor(dateTimeService, trackingKeyGenerator).Code is
                ResultCodes.OK or
                ResultCodes.NOT_MODIFIED))
        {
            return this.ApplyEvent(@event: @event, isNew: true);
        }
        else
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED, "The two-factor authentication configuration failed.");
        }
    }

    protected Result RevokeRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            roleKey == new RoleKey(string.Empty))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleRevoked(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key,
            RoleKey: roleKey);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result EnableTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorEnabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected Result DisableTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorDisabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            AccountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    protected override Result DispatchEvent(IDomainEventModel @event)
    {
        var result = @event switch
        {
            AccountRegistered e => this.OnAccountRegistered(e),
            AccountActivated e => this.OnAccountActivated(e),
            AccountDeactivated e => this.OnAccountDeactivated(e),
            AccountRestricted e => this.OnAccountRestricted(e),
            AccountClosed e => this.OnAccountClosed(e),
            AccountBanned e => this.OnAccountBanned(e),
            RoleAssigned e => this.OnRoleAssigned(e),
            RoleRevoked e => this.OnRoleRevoked(e),
            TwoFactorEnabled e => this.OnTwoFactorEnabled(e),
            TwoFactorDisabled e => this.OnTwoFactorDisabled(e),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnAccountRegistered(AccountRegistered @event)
    {
        this.DisplayName = @event.DisplayName;
        this.State = @event.AccountState;
        this.Type = @event.AccountType;
        this.TwoFactorEnabled = false;

        return Result.Completed(@event.AccountKey);
    }

    private Result OnAccountActivated(AccountActivated _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.ValidateStateTransition(AccountStates.ACTIVATED, out Result result))
        {
            return result;
        }

        this.State = AccountStates.ACTIVATED;

        return Result.Completed();
    }

    private Result OnAccountDeactivated(AccountDeactivated _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.ValidateStateTransition(AccountStates.DEACTIVATED, out Result result))
        {
            return result;
        }

        this.State = AccountStates.DEACTIVATED;

        return Result.Completed();
    }

    private Result OnAccountRestricted(AccountRestricted _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.ValidateStateTransition(AccountStates.RESTRICTED, out Result result))
        {
            return result;
        }

        this.State = AccountStates.RESTRICTED;

        return Result.Completed();
    }

    private Result OnAccountClosed(AccountClosed _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.ValidateStateTransition(AccountStates.CLOSED, out Result result))
        {
            return result;
        }

        this.State = AccountStates.CLOSED;

        return Result.Completed();
    }

    private Result OnAccountBanned(AccountBanned _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (!this.ValidateStateTransition(AccountStates.BANNED, out Result result))
        {
            return result;
        }

        this.State = AccountStates.BANNED;

        return Result.Completed();
    }

    private Result OnRoleAssigned(RoleAssigned @event)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State is AccountStates.CLOSED or AccountStates.BANNED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY);
        }

        var accountRole = this.FindAccountRole(@event.RoleKey);

        if (accountRole is not null)
        {
            return Result.Terminated(
                code: ResultCodes.NOT_MODIFIED,
                message: "The role is already assigned to the account.");
        }

        var result = AccountRole.Assign(@event.RoleKey);

        if (result.State is ResultStates.COMPLETED)
        {
            result.TryGetOutcome<AccountRole>(out var newAccountRole);
            this.assignedRoles.Add(newAccountRole!);
        }

        return result;
    }

    private Result OnRoleRevoked(RoleRevoked @event)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State is AccountStates.CLOSED or AccountStates.BANNED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY);
        }

        var accountRole = this.FindAccountRole(@event.RoleKey);

        if (accountRole is null)
        {
            return Result.Terminated(
                code: ResultCodes.NOT_FOUND,
                message: "The role is not assigned to the account.");
        }

        return accountRole.Revoke();
    }

    private Result OnTwoFactorEnabled(TwoFactorEnabled _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State is AccountStates.CLOSED or AccountStates.BANNED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY);
        }

        this.TwoFactorEnabled = true;

        return Result.Completed();
    }

    private Result OnTwoFactorDisabled(TwoFactorDisabled _)
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.ARCHIVED);
        }
        else if (this.State is AccountStates.CLOSED or AccountStates.BANNED)
        {
            return Result.Terminated(ResultCodes.INCONSISTENCY);
        }

        this.TwoFactorEnabled = false;

        return Result.Completed();
    }

    private bool ValidateStateTransition(AccountStates newState, out Result result)
    {
        //TODO: This method should be extended/refactored to validate state transitions based on business rules.

        if (this.State == newState)
        {
            result = Result.Terminated(
                code: ResultCodes.NOT_MODIFIED,
                message: "The account is already in the specified state.");

            return false;
        }
        else if (this.Type is AccountTypes.GUEST or AccountTypes.SYSTEM)
        {
            result = Result.Terminated(
                code: ResultCodes.NOT_MODIFIED,
                message: "You cannot change the guest and system account's states.");

            return false;
        }
        else if (this.State is AccountStates.CLOSED or AccountStates.BANNED)
        {
            result = Result.Terminated(
                code: ResultCodes.INCONSISTENCY,
                message: "You cannot change the state of closed or banned accounts.");

            return false;
        }

        result = Result.Completed();

        return true;
    }

    private AccountRole? FindAccountRole(RoleKey roleKey)
    {
        return this.assignedRoles.FirstOrDefault(r => r.RoleKey == roleKey);
    }
}
