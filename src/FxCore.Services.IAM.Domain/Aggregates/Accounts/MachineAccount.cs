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
/// Defines the machine account concrete aggregate root.
/// </summary>
public sealed class MachineAccount : Account<MachineAccount>
{
    private MachineAccount()
        : base()
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

    /// <summary>
    /// Registers a new machine account.
    /// </summary>
    /// <param name="dateTimeService">A date and time service provider.</param>
    /// <param name="trackingKeyGenerator">A tracking key generator.</param>
    /// <param name="machineAccountKeyGenerator">A machine account key generator.</param>
    /// <param name="displayName">The machine account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
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

    /// <summary>
    /// Activates the machine account.
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
    /// Deactivates the machine account.
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
    /// Restricts the machine account.
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
    /// Closes the machine account.
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
    /// Bans the machine account.
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
    /// Assigns a role to the machine account.
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
                message: "Machine accounts cannot be assigned to sensitive roles.");
        }

        return base.AssignRole(dateTimeService, trackingKeyGenerator, roleKey, isSensitiveRole);
    }

    /// <summary>
    /// Revokes a role from the machine account.
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
