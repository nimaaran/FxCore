// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines the base class for different types of accounts.
/// </summary>
/// <typeparam name="TAggregateRootModel">Type of the concrete aggregate root model.</typeparam>
public abstract class Account<TAggregateRootModel> : EventDrivenRootBase<long, AccountKey>
    where TAggregateRootModel : IAggregateRootModel
{
    private readonly List<AccountRole> assignedRoles = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Account{TAggregateRootModel}"/> class.
    /// </summary>
    protected Account()
        : base(
            id: default,
            key: new AccountKey(string.Empty),
            removed: false,
            @lock: AggregateLock.Empty)
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="Account{TAggregateRootModel}"/> class.
    /// </summary>
    /// <param name="dateTimeService">A date nd time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="accountKeyGenerator">An account key generator.</param>
    /// <param name="displayName">The account display name.</param>
    /// <param name="accountType">The account type.</param>
    /// <param name="accountState">The account state.</param>
    /// <param name="result">An object as type of <see cref="Result"/>.</param>
    protected Account(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IAccountKeyGenerator<TAggregateRootModel> accountKeyGenerator,
        string displayName,
        AccountTypes accountType,
        AccountStates accountState,
        out Result result)
        : this(
            accountKey: accountKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dateTimeService.Now()))
    {
        var @event = new AccountRegistered(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: this.Lock.Timestamp,
            Key: this.Key,
            DisplayName: displayName,
            Type: accountType,
            State: accountState);

        result = this.ApplyEvent(@event: @event, isNew: true);
    }

    private Account(AccountKey accountKey, AggregateLock @lock)
        : base(
            id: default,
            key: accountKey,
            removed: false,
            @lock: @lock)
    {
    }

    /// <summary>
    /// Gets the account display name.
    /// </summary>
    public string DisplayName { get; private set; } = string.Empty;

    /// <summary>
    /// Gets a value indicating whether two-factor authentication is enabled or not.
    /// </summary>
    public bool TwoFactorEnabled { get; private set; } = false;

    /// <summary>
    /// Gets the account type.
    /// </summary>
    public AccountTypes Type { get; private set; } = AccountTypes.GUEST;

    /// <summary>
    /// Gets the account state.
    /// </summary>
    public AccountStates State { get; private set; } = AccountStates.REGISTERED;

    /// <summary>
    /// Gets a readonly collection of assigned roles.
    /// </summary>
    public IReadOnlyCollection<AccountRole> AssignedRoles => this.assignedRoles.AsReadOnly();

    /// <summary>
    /// Activates the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Activate(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountActivated(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Deactivates the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Deactivate(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountDeactivated(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Restricts the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Restrict(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountRestricted(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Closes the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Close(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountClosed(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Bans the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Ban(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountBanned(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Assigns a role to the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="roleKey">Desired role key.</param>
    /// <param name="isSensitiveRole">A flag indicating whether the role is sensitive.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result AssignRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey,
        bool isSensitiveRole)
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

        if (isSensitiveRole && !this.TwoFactorEnabled)
        {
            return Result.Terminated(
                code: ResultCodes.NOT_MODIFIED,
                message: "The two-factor authentication is not enabled.");
        }

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Revokes a role from the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="roleKey">Desired role's key.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
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

    /// <summary>
    /// Enables two-factor authentication for the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result EnableTwoFactor(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorEnabled(
            TrackingKey: trackingKeyGenerator.Generate(),
            Timestamp: dateTimeService.Now(),
            Key: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Disables two-factor authentication for the account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result DisableTwoFactor(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorDisabled(
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
            AccountRegistered e => this.OnAccountRegistered(e),
            AccountActivated _ => this.OnAccountActivated(),
            AccountDeactivated _ => this.OnAccountDeactivated(),
            AccountRestricted _ => this.OnAccountRestricted(),
            AccountClosed _ => this.OnAccountClosed(),
            AccountBanned _ => this.OnAccountBanned(),
            TwoFactorEnabled _ => this.OnTwoFactorEnabled(),
            TwoFactorDisabled _ => this.OnTwoFactorDisabled(),
            RoleAssigned e => this.OnRoleAssigned(e),
            RoleRevoked e => this.OnRoleRevoked(e),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private Result OnAccountRegistered(AccountRegistered @event)
    {
        this.DisplayName = @event.DisplayName;
        this.State = @event.State;
        this.Type = @event.Type;
        this.TwoFactorEnabled = false;

        return Result.Completed(@event.Key);
    }

    private Result OnAccountActivated()
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

    private Result OnAccountDeactivated()
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

    private Result OnAccountRestricted()
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

    private Result OnAccountClosed()
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

    private Result OnAccountBanned()
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

    private Result OnTwoFactorEnabled()
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

    private Result OnTwoFactorDisabled()
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
        // TODO: This method should be extended/refactored to validate state transitions based on business rules.
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
