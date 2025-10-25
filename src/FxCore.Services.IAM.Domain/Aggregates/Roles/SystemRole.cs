// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Roles;

/// <summary>
/// Implements a concrete aggregate root class for system roles.
/// </summary>
public sealed class SystemRole : Role<SystemRole>
{
    private SystemRole()
        : base()
    {
    }

    private SystemRole(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator<SystemRole> roleKeyGenerator,
        string title,
        out Result result)
        : base(
            dependencies,
            roleKeyGenerator,
            title,
            RoleTypes.SYSTEM_DEFINED,
            RoleStates.DISABLED,
            out result)
    {
    }

    /// <summary>
    /// Defines a new system role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <param name="roleKeyGenerator">A role key generator service.</param>
    /// <param name="title">See <see cref="Role{TRole}.Title"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Define(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator<SystemRole> roleKeyGenerator,
        string title)
    {
        if (dependencies is null ||
            roleKeyGenerator is null ||
            string.IsNullOrWhiteSpace(title))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new SystemRole(
            dependencies,
            roleKeyGenerator,
            title,
            out Result result);

        return result;
    }
}
