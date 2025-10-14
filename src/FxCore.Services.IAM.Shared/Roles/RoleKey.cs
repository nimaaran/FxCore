using FxCore.Abstraction.Models;

namespace FxCore.Services.IAM.Shared.Roles;

public record class RoleKey(string Value) : AggregateKeyBase<string>(Value);

