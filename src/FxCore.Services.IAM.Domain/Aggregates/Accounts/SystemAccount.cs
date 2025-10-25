// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
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
        IEventDependenciesProvider dependencies,
        IAccountKeyGenerator<SystemAccount> systemAccountKeyGenerator,
        string displayName,
        out Result result)
        : base(
            dependencies,
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
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="systemAccountKeyGenerator">A system account key generator.</param>
    /// <param name="displayName">The system account display name.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IEventDependenciesProvider dependencies,
        IAccountKeyGenerator<SystemAccount> systemAccountKeyGenerator,
        string displayName)
    {
        if (dependencies is null ||
            systemAccountKeyGenerator is null ||
            string.IsNullOrWhiteSpace(displayName))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new SystemAccount(
            dependencies,
            systemAccountKeyGenerator,
            displayName,
            out Result result);

        return result;
    }

    /// <summary>
    /// Assigns a role to the system account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="AccountRole.RoleKey"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result AssignRole(IEventDependenciesProvider dependencies, RoleKey roleKey)
    {
        return base.AssignRole(dependencies, roleKey);
    }

    /// <summary>
    /// Revokes a role from the system account.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKey">See <see cref="AccountRole.RoleKey"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result RevokeRole(IEventDependenciesProvider dependencies, RoleKey roleKey)
    {
        return base.RevokeRole(dependencies, roleKey);
    }
}
