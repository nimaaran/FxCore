// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;
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
        IEventDependenciesProvider dependencies,
        IMachineAccountKeyGenerator machineAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dependencies,
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
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="machineAccountKeyGenerator">A machine account key generator.</param>
    /// <param name="displayName">The machine account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IEventDependenciesProvider dependencies,
        IMachineAccountKeyGenerator machineAccountKeyGenerator,
        string displayName)
    {
        if (dependencies is null ||
            machineAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new MachineAccount(
            dependencies,
            machineAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Activates the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Activate(IEventDependenciesProvider dependencies)
    {
        return base.Activate(dependencies);
    }

    /// <summary>
    /// Deactivates the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Deactivate(IEventDependenciesProvider dependencies)
    {
        return base.Deactivate(dependencies);
    }

    /// <summary>
    /// Closes the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Close(IEventDependenciesProvider dependencies)
    {
        return base.Close(dependencies);
    }

    /// <summary>
    /// Bans the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Ban(IEventDependenciesProvider dependencies)
    {
        return base.Ban(dependencies);
    }

    /// <summary>
    /// Assigns a role to the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="roleKey">Desired role key.</param>
    /// <param name="isSensitiveRole">A flag indicating whether the role is sensitive.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result AssignRole(
        IEventDependenciesProvider dependencies,
        RoleKey roleKey,
        bool isSensitiveRole)
    {
        if (isSensitiveRole)
        {
            return Result.Terminated(
                code: ResultCodes.INCONSISTENCY,
                message: "Machine accounts cannot be assigned to sensitive roles.");
        }

        return base.AssignRole(dependencies, roleKey, isSensitiveRole);
    }

    /// <summary>
    /// Revokes a role from the machine account.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="roleKey">Desired role's key.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result RevokeRole(
        IEventDependenciesProvider dependencies,
        RoleKey roleKey)
    {
        return base.RevokeRole(dependencies, roleKey);
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
    public new Result PassportVerified(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfigProvider configs,
        PassportTypes passportType)
    {
        return base.PassportVerified(dependencies, configs, passportType);
    }

    /// <summary>
    /// Evaluates the state of the account after a failed authentication attempt.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="configs">An object that contains the required configs.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result PassportVerificationFailed(
        IEventDependenciesProvider dependencies,
        IAuthenticationConfigProvider configs)
    {
        return base.PassportVerificationFailed(dependencies, configs);
    }
}
