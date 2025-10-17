// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
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
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IUserAccountKeyGenerator userAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dateTimeService,
            trackingKeyGenerator,
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
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="userAccountKeyGenerator">A user account key generator.</param>
    /// <param name="displayName">The user account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IUserAccountKeyGenerator userAccountKeyGenerator,
        string displayName)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            userAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new UserAccount(
            dateTimeService,
            trackingKeyGenerator,
            userAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Activates the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Activate(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Activate(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Deactivates the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Deactivate(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Deactivate(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Restricts the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Restrict(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Restrict(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Closes the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Close(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Close(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Bans the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Ban(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Ban(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Assigns a role to the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="roleKey">Desired role key.</param>
    /// <param name="isSensitiveRole">A flag indicating whether the role is sensitive.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result AssignRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey,
        bool isSensitiveRole)
    {
        return base.AssignRole(dateTimeService, trackingKeyGenerator, roleKey, isSensitiveRole);
    }

    /// <summary>
    /// Revokes a role from the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="roleKey">Desired role's key.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result RevokeRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey)
    {
        return base.RevokeRole(dateTimeService, trackingKeyGenerator, roleKey);
    }

    /// <summary>
    /// Enables two-factor authentication for the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result EnableTwoFactor(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.EnableTwoFactor(dateTimeService, trackingKeyGenerator);
    }

    /// <summary>
    /// Disables two-factor authentication for the user account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result DisableTwoFactor(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.DisableTwoFactor(dateTimeService, trackingKeyGenerator);
    }
}
