using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed class MachineAccount : Account<MachineAccount>
{
    private MachineAccount() : base()
    {
    }

    private MachineAccount(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IMachineAccountKeyGenerator machineAccountKeyGenerator,
        string displayName,
        out Result result) 
        : base(
            dateTimeService, 
            trackingKeyGenerator, 
            machineAccountKeyGenerator, 
            displayName, 
            AccountTypes.MACHINE, 
            AccountStates.REGISTERED, 
            out result)
    {
    }

    public static Result Register(
        IDateTimeService dateTimeService,
        ITrackingKeyGenerator trackingKeyGenerator,
        IMachineAccountKeyGenerator machineAccountKeyGenerator,
        string displayName)
    {
        if (dateTimeService is null ||
        trackingKeyGenerator is null ||
        machineAccountKeyGenerator is null ||
        string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new MachineAccount(
            dateTimeService,
            trackingKeyGenerator,
            machineAccountKeyGenerator,
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

    public new Result RevokeRole(IDateTimeService dateTimeService, ITrackingKeyGenerator trackingKeyGenerator, RoleKey roleKey)
    {
        return base.RevokeRole(dateTimeService, trackingKeyGenerator, roleKey);
    }
}
