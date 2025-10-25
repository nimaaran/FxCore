// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Entities;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

/// <summary>
/// Defines the association between an account and a role.
/// </summary>
public sealed class AccountRole : EntityBase<long>
{
    private AccountRole()
        : base(id: default, removed: false)
    {
    }

    private AccountRole(RoleKey roleKey)
        : this() => this.RoleKey = roleKey;

    /// <summary>
    /// Gets the aggregate key of the role assigned to the account.
    /// </summary>
    public RoleKey RoleKey { get; private set; } = new(string.Empty);

    internal static Result Assign(RoleKey roleKey)
    {
        var accountRole = new AccountRole(roleKey);

        return Result.Completed(accountRole);
    }

    internal Result Revoke()
    {
        return this.Remove();
    }
}
