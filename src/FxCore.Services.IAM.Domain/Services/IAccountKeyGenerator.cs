// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for accout key generators.
/// </summary>
/// <typeparam name="TAggregateRootModel">Type of the aggregate root model.</typeparam>
public interface IAccountKeyGenerator<TAggregateRootModel>
    : IAggregateKeyGenerator<TAggregateRootModel, AccountKey>
    where TAggregateRootModel : IAggregateRootModel;
