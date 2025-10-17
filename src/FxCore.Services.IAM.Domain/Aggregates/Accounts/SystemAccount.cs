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
/// Defines the system account concrete aggregate root.
/// </summary>
public sealed class SystemAccount : Account<SystemAccount>
{
    private SystemAccount()
        : base()
    {
    }

    private SystemAccount(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        ISystemAccountKeyGenerator systemAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dateTimeService,
            trackingKeyGenerator,
            systemAccountKeyGenerator,
            displayName,
            AccountTypes.SYSTEM,
            AccountStates.ACTIVATED,
            out result)
    {
    }

    /// <summary>
    /// Registers a new system account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="systemAccountKeyGenerator">A system account key generator.</param>
    /// <param name="displayName">The system account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        ISystemAccountKeyGenerator systemAccountKeyGenerator,
        string displayName)
    {
        if (dateTimeService is null ||
            trackingKeyGenerator is null ||
            systemAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new SystemAccount(
            dateTimeService,
            trackingKeyGenerator,
            systemAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Assigns a role to the system account.
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
    /// Revokes a role from the system account.
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
