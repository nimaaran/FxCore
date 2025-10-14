using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed class UserAccount : Account<UserAccount>
{
    private UserAccount() : base()
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

    public new Result Activate(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Activate(dateTimeService, trackingKeyGenerator);
    }

    public new Result Deactivate(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Deactivate(dateTimeService, trackingKeyGenerator);
    }

    public new Result Restrict(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Restrict(dateTimeService, trackingKeyGenerator);
    }

    public new Result Close(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Close(dateTimeService, trackingKeyGenerator);
    }

    public new Result Ban(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.Ban(dateTimeService, trackingKeyGenerator);
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

    public new Result EnableTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.EnableTwoFactor(dateTimeService, trackingKeyGenerator);
    }

    public new Result DisableTwoFactor(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator)
    {
        return base.DisableTwoFactor(dateTimeService, trackingKeyGenerator);
    }
}
