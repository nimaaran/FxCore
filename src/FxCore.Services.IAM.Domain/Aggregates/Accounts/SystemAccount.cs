using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed class SystemAccount : Account<SystemAccount>
{
    private SystemAccount() : base()
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

    public new Result AssignRole(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        RoleKey roleKey,
        bool twoFactorRequired)
    {
        return base.AssignRole(dateTimeService, trackingKeyGenerator, roleKey, twoFactorRequired);
    }

    public new Result RevokeRole(
        IDateTimeService dateTimeService, 
        ITrackingKeyGenerator trackingKeyGenerator, 
        RoleKey roleKey)
    {
        return base.RevokeRole(dateTimeService, trackingKeyGenerator, roleKey);
    }
}
