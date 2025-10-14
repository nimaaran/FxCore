using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Domain.Aggregates.Roles;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Services;

public interface IRoleKeyGenerator : IAggregateKeyGenerator<Role, RoleKey>;