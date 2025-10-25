// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Accounts;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for accout key generators.
/// </summary>
/// <typeparam name="TAccount">Type of the account.</typeparam>
public interface IAccountKeyGenerator<TAccount> : IAggregateKeyGenerator<TAccount, AccountKey>
    where TAccount : Account<TAccount>;
