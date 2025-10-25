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
/// Implements a concrete aggregate root class for user roles.
/// </summary>
public class UserRole : Role<UserRole>
{
    private UserRole()
        : base()
    {
    }

    private UserRole(
        IEventDependenciesProvider dependencies,
        IRoleKeyGenerator<UserRole> roleKeyGenerator,
        string title,
        out Result result)
        : base(
            dependencies,
            roleKeyGenerator,
            title,
            RoleTypes.USER_DEFINED,
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
        IRoleKeyGenerator<UserRole> roleKeyGenerator,
        string title)
    {
        if (dependencies is null ||
            roleKeyGenerator is null ||
            string.IsNullOrWhiteSpace(title))
        {
            return Result.Terminated(ResultCodes.BAD_REQUEST);
        }

        _ = new UserRole(
            dependencies,
            roleKeyGenerator,
            title,
            out Result result);

        return result;
    }

    /// <summary>
    /// Enables the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Enable(IEventDependenciesProvider dependencies)
    {
        return base.Enable(dependencies);
    }

    /// <summary>
    /// Disables the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Disable(IEventDependenciesProvider dependencies)
    {
        return base.Disable(dependencies);
    }

    /// <summary>
    /// Removes the role.
    /// </summary>
    /// <param name="dependencies">See <see cref="IEventDependenciesProvider"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public new Result Remove(IEventDependenciesProvider dependencies)
    {
        return base.Remove(dependencies);
    }
}
