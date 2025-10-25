// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines the user account concrete aggregate root.
/// </summary>
public sealed class UserAccount : Account<UserAccount>
{
    private UserAccount()
        : base()
    {
    }

    private UserAccount(
        IEventDependenciesProvider dependencies,
        IAccountKeyGenerator<UserAccount> userAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dependencies,
            userAccountKeyGenerator,
            displayName,
            AccountTypes.USER,
            AccountStates.REGISTERED,
            out result)
    {
    }

    /// <summary>
    /// Registers a new user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="userAccountKeyGenerator">A user account key generator.</param>
    /// <param name="displayName">The user account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IEventDependenciesProvider dependencies,
        IAccountKeyGenerator<UserAccount> userAccountKeyGenerator,
        string displayName)
    {
        if (dependencies is null ||
            userAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new UserAccount(
            dependencies,
            userAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Activates the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Activate(IEventDependenciesProvider dependencies)
    {
        return base.Activate(dependencies);
    }

    /// <summary>
    /// Deactivates the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Deactivate(IEventDependenciesProvider dependencies)
    {
        return base.Deactivate(dependencies);
    }

    /// <summary>
    /// Closes the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Close(IEventDependenciesProvider dependencies)
    {
        return base.Close(dependencies);
    }

    /// <summary>
    /// Bans the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Ban(IEventDependenciesProvider dependencies)
    {
        return base.Ban(dependencies);
    }

    /// <summary>
    /// Assigns a role to the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="AccountRole.RoleKey"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result AssignRole(IEventDependenciesProvider dependencies, RoleKey roleKey)
    {
        return base.AssignRole(dependencies, roleKey);
    }

    /// <summary>
    /// Revokes a role from the user account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="AccountRole.RoleKey"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result RevokeRole(IEventDependenciesProvider dependencies, RoleKey roleKey)
    {
        return base.RevokeRole(dependencies, roleKey);
    }

    /// <summary>
    /// Enables two-factor authentication for the user account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result EnableTwoFactor(IEventDependenciesProvider dependencies)
    {
        return base.EnableTwoFactor(dependencies);
    }

    /// <summary>
    /// Disables two-factor authentication for the user account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result DisableTwoFactor(IEventDependenciesProvider dependencies)
    {
        return base.DisableTwoFactor(dependencies);
    }

    /// <summary>
    /// Evaluates the state of the account after a successful authentication attempt.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="configs">An object that contains the required configs.</param>
    /// <param name="passportType">The type of a passport that used for authentication.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result PassportVerified(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs,
        PassportTypes passportType)
    {
        return base.PassportVerified(dependencies, configs, passportType);
    }

    /// <summary>
    /// Evaluates the state of the account after a failed authentication attempt.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="configs">An object that contains the required configs.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result PassportVerificationFailed(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfiguration configs)
    {
        return base.PassportVerificationFailed(dependencies, configs);
    }
}
