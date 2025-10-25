// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence.Repositories.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Roles;

namespace FxCore.Services.IAM.Domain.Repositories;

/// <summary>
/// Defines a contract to define query methods of role aggregate repositories.
/// </summary>
/// <typeparam name="TRole">Type of the passport.</typeparam>
public interface IRoleQueriesRepository<TRole> :
    IAggregateQueriesRepository<TRole>
    where TRole : Role<TRole>;
