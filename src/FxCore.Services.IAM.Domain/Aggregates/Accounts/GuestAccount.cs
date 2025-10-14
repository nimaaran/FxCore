using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed class GuestAccount : Account<GuestAccount>
{
    private GuestAccount() : base() 
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
