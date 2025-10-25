// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Roles;
using FxCore.Services.IAM.Shared.Roles;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for role key generators.
/// </summary>
/// <typeparam name="TRole">Type of the role.</typeparam>
public interface IRoleKeyGenerator<TRole> : IAggregateKeyGenerator<TRole, RoleKey>
    where TRole : Role<TRole>;
