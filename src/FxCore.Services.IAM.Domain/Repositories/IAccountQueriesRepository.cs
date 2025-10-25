// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence.Repositories.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Accounts;

namespace FxCore.Services.IAM.Domain.Repositories;

/// <summary>
/// Defines a contract to define query methods of account aggregate repositories.
/// </summary>
/// <typeparam name="TAccount">the account aggregate root type.</typeparam>
public interface IAccountQueriesRepository<TAccount> :
    IAggregateQueriesRepository<TAccount>
    where TAccount : Account<TAccount>;
