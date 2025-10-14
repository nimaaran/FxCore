using FxCore.Abstraction.Models;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Aggregates.Accounts;

public sealed class AccountRole : EntityBase<long>
{
    private AccountRole() : base(id: default, removed: false)
    {
    }

    private AccountRole(RoleKey roleKey) : this()
    {
        this.RoleKey = roleKey;
    }

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
