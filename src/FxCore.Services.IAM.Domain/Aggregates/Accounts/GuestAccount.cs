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
/// Defines the guest account concrete aggregate root.
/// </summary>
public sealed class GuestAccount : Account<GuestAccount>
{
    private GuestAccount()
        : base()
    {
    }

    private GuestAccount(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IGuestAccountKeyGenerator guestAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dateTimeService,
            trackingKeyGenerator,
            guestAccountKeyGenerator,
            displayName,
            AccountTypes.GUEST,
            AccountStates.ACTIVATED,
            out result)
    {
    }

    /// <summary>
    /// Registers a new guest account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="guestAccountKeyGenerator">A guest account key generator.</param>
    /// <param name="displayName">The guest account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IGuestAccountKeyGenerator guestAccountKeyGenerator,
        string displayName)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            guestAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new GuestAccount(
            dateTimeService,
            trackingKeyGenerator,
            guestAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Assigns a role to the guest account.
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
        if (isSensitiveRole)
        {
            return Result.Terminated(
                code: ResultCodes.INCONSISTENCY,
                message: "Guest accounts cannot be assigned to sensitive roles.");
        }

        return base.AssignRole(dateTimeService, trackingKeyGenerator, roleKey, isSensitiveRole);
    }

    /// <summary>
    /// Revokes a role from the guest account.
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
}
