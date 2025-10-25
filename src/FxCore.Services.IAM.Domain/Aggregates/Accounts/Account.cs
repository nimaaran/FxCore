// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates;
using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Events.Accounts;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines the base class for concrete account types.
/// </summary>
/// <typeparam name="TAccount">the account type.</typeparam>
public abstract class Account<TAccount> : EventDrivenRootBase<long, AccountKey>
    where TAccount : Account<TAccount>
{
    private readonly List<AccountRole> assignedRoles = [];

    /// <summary>
    /// Initializes a new instance of the <see cref="Account{TAccount}"/> class.
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
    /// Initializes a new instance of the <see cref="Account{TAccount}"/> class.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="accountKeyGenerator">An account key generator.</param>
    /// <param name="displayName">The account display name.</param>
    /// <param name="accountType">The account type.</param>
    /// <param name="accountState">The account state.</param>
    /// <param name="result">An object as type of <see cref="Result"/>.</param>
    protected Account(
        IEventDependenciesProvider dependencies,
        IAccountKeyGenerator<TAccount> accountKeyGenerator,
        string displayName,
        AccountTypes accountType,
        AccountStates accountState,
        out Result result)
        : this(
            accountKey: accountKeyGenerator.Generate(),
            @lock: AggregateLock.Create(dependencies.DateTimeService.UtcNow()))
    {
        var @event = new AccountRegistered(
            dependencies,
            accountKey: this.Key,
            displayName: displayName,
            type: accountType,
            state: accountState);

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
    /// Gets the number of failed login attempts.
    /// </summary>
    public byte FailedLoginAttemptsCounter { get; private set; } = 0;

    /// <summary>
    /// Gets the timestamp of the last login attempt.
    /// </summary>
    public DateTimeOffset LastLoginAttempt { get; private set; } = DateTimeOffset.MinValue;

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
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Activate(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountActivated(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Deactivates the account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Deactivate(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountDeactivated(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Closes the account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Close(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountClosed(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Bans the account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result Ban(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new AccountBanned(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Assigns a role to the account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">Desired role key.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result AssignRole(
        IEventDependenciesProvider dependencies,
        RoleKey roleKey)
    {
        if (dependencies is null || roleKey == new RoleKey(string.Empty))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleAssigned(
            dependencies: dependencies,
            accountKey: this.Key,
            roleKey: roleKey);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Revokes a role from the account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="roleKey">Desired role's key.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result RevokeRole(IEventDependenciesProvider dependencies, RoleKey roleKey)
    {
        if (dependencies is null ||
            roleKey == new RoleKey(string.Empty))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new RoleRevoked(
            dependencies: dependencies,
            accountKey: this.Key,
            roleKey: roleKey);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Enables two-factor authentication for the account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result EnableTwoFactor(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorEnabled(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Disables two-factor authentication for the account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result DisableTwoFactor(IEventDependenciesProvider dependencies)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        var @event = new TwoFactorDisabled(
            dependencies: dependencies,
            accountKey: this.Key);

        return this.ApplyEvent(@event: @event, isNew: true);
    }

    /// <summary>
    /// Evaluates the state of the account after a successful authentication attempt.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="configs">An object that contains the required configs.</param>
    /// <param name="passportType">The type of a passport that used for authentication.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result PassportVerified(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs,
        PassportTypes passportType)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }
        else if (this.State is AccountStates.BANNED or
                               AccountStates.CLOSED)
        {
            return Result.Terminated(
                code: ResultCodes.FORBIDDEN,
                message: "The closed or banned account is not allowed to authenticate.");
        }

        Result result;

        // Handles inactivated, suspended, and protected accounts
        if (!this.TwoFactorEnabled || passportType is PassportTypes.PHONE or
                                                      PassportTypes.EMAIL)
        {
            if (!this.HandleInactiveAccounts(dependencies, out result) ||
                !this.HandleSuspendedAccounts(dependencies, configs, out result) ||
                !this.HandleProtectedAccounts(dependencies, configs, out result))
            {
                return result;
            }
        }

        var twoFactorAuthenticationEnd = this.LastLoginAttempt.Add(configs.TwoFactorStepsGapDuration);

        if (this.TwoFactorEnabled &&
            twoFactorAuthenticationEnd <= dependencies.DateTimeService.UtcNow() &&
            passportType is PassportTypes.EMAIL or
                            PassportTypes.PHONE)
        {
            return Result.Terminated(
                code: ResultCodes.FORBIDDEN,
                message: "The user should start authentication again.");
        }

        if (this.TwoFactorEnabled && passportType is not PassportTypes.EMAIL and
                                                         PassportTypes.PHONE)
        {
            var @event = new TwoFactorAuthenticationRequired(
                dependencies: dependencies,
                accountKey: this.Key);

            result = this.ApplyEvent(@event, isNew: true);
        }
        else
        {
            var @event = new AccountAuthenticated(
                dependencies: dependencies,
                accountKey: this.Key);

            result = this.ApplyEvent(@event, isNew: true);
        }

        return result;
    }

    /// <summary>
    /// Evaluates the state of the account after a failed authentication attempt.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="configs">An object that contains the required configs.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    protected Result PassportVerificationFailed(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs)
    {
        if (dependencies is null)
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }
        else if (this.State is AccountStates.BANNED or
                               AccountStates.CLOSED)
        {
            return Result.Terminated(
                code: ResultCodes.FORBIDDEN,
                message: "The closed or banned account is not allowed to authenticate.");
        }
        else if (this.FailedLoginAttemptsCounter + 1 == configs.SuspensionThreshold)
        {
            var @event = new AccountSuspended(
                dependencies: dependencies,
                accountKey: this.Key);

            return this.ApplyEvent(@event, isNew: true);
        }
        else if (this.FailedLoginAttemptsCounter + 1 == configs.ProtectionThreshold)
        {
            var @event = new AccountProtected(
                dependencies: dependencies,
                accountKey: this.Key);

            return this.ApplyEvent(@event, isNew: true);
        }
        else
        {
            var @event = new AuthenticationFailed(
                dependencies: dependencies,
                accountKey: this.Key);

            return this.ApplyEvent(@event, isNew: true);
        }
    }

    /// <inheritdoc/>
    protected override Result DispatchEvent(IDomainEvent @event)
    {
        var result = @event switch
        {
            AccountRegistered e => this.OnAccountRegistered(e),
            AccountActivated _ => this.OnAccountActivated(),
            AccountDeactivated _ => this.OnAccountDeactivated(),
            AccountClosed _ => this.OnAccountClosed(),
            AccountBanned _ => this.OnAccountBanned(),
            TwoFactorEnabled _ => this.OnTwoFactorEnabled(),
            TwoFactorDisabled _ => this.OnTwoFactorDisabled(),
            RoleAssigned e => this.OnRoleAssigned(e),
            RoleRevoked e => this.OnRoleRevoked(e),
            AccountProtected e => this.OnAccountProtected(e),
            AccountSuspended e => this.OnAccountSuspended(e),
            AuthenticationFailed e => this.OnAuthenticationFailed(e),
            AccountAuthenticated e => this.OnAccountAuthenticated(e),
            TwoFactorAuthenticationRequired _ => this.OnTwoFactorAuthenticationRequired(),

            _ => base.DispatchEvent(@event),
        };

        return result;
    }

    private bool HandleInactiveAccounts(
        IEventDependenciesProvider dependencies,
        out Result result)
    {
        if (this.State is AccountStates.REGISTERED or
                          AccountStates.DEACTIVATED)
        {
            result = this.Activate(dependencies);

            if (result.State != ResultStates.COMPLETED)
            {
                return false;
            }
        }

        result = Result.Completed();

        return true;
    }

    private bool HandleSuspendedAccounts(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs,
        out Result result)
    {
        if (this.State is AccountStates.SUSPENDED)
        {
            if (this.LastLoginAttempt.Add(configs.SuspensionDuration) >= dependencies.DateTimeService.UtcNow())
            {
                result = Result.Terminated(
                    code: ResultCodes.FORBIDDEN,
                    message: "The account is suspended.");

                return false;
            }
        }

        result = Result.Completed();

        return true;
    }

    private bool HandleProtectedAccounts(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs,
        out Result result)
    {
        if (this.State is AccountStates.PROTECTED)
        {
            if (this.LastLoginAttempt.Add(configs.ProtectionDuration) >= dependencies.DateTimeService.UtcNow())
            {
                result = Result.Terminated(
                    code: ResultCodes.FORBIDDEN,
                    message: "The account is protected.");

                return false;
            }
        }

        result = Result.Completed();

        return true;
    }

    private Result OnAccountRegistered(AccountRegistered @event)
    {
        this.DisplayName = @event.DisplayName;
        this.State = @event.State;
        this.Type = @event.Type;
        this.TwoFactorEnabled = false;

        return Result.Completed(@event.AccountKey);
    }

    private Result OnAccountProtected(AccountProtected @event)
    {
        this.State = AccountStates.PROTECTED;
        this.FailedLoginAttemptsCounter += 1;
        this.LastLoginAttempt = @event.Timestamp;

        return Result.Completed();
    }

    private Result OnAccountSuspended(AccountSuspended @event)
    {
        this.State = AccountStates.SUSPENDED;
        this.FailedLoginAttemptsCounter += 1;
        this.LastLoginAttempt = @event.Timestamp;

        return Result.Completed();
    }

    private Result OnAuthenticationFailed(AuthenticationFailed @event)
    {
        this.FailedLoginAttemptsCounter += 1;
        this.LastLoginAttempt = @event.Timestamp;

        return Result.Completed();
    }

    private Result OnAccountAuthenticated(AccountAuthenticated @event)
    {
        this.FailedLoginAttemptsCounter = default;
        this.LastLoginAttempt = @event.Timestamp;

        return Result.Completed();
    }

    private Result OnTwoFactorAuthenticationRequired()
    {
        return Result.Completed();
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
            return Result.Terminated(ResultCodes.FORBIDDEN);
        }

        var accountRole = this.FindActiveAccountRole(@event.RoleKey);

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
            return Result.Terminated(ResultCodes.FORBIDDEN);
        }

        var accountRole = this.FindActiveAccountRole(@event.RoleKey);

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
            return Result.Terminated(ResultCodes.FORBIDDEN);
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
            return Result.Terminated(ResultCodes.FORBIDDEN);
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
                code: ResultCodes.FORBIDDEN,
                message: "You cannot change the state of closed or banned accounts.");

            return false;
        }

        result = Result.Completed();

        return true;
    }

    private AccountRole? FindActiveAccountRole(RoleKey roleKey)
    {
        return this.assignedRoles.FirstOrDefault(r => r.RoleKey == roleKey &&
                                                      r.Removed == false);
    }
}
