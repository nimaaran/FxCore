// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;
using FxCore.Abstraction.Services;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for passport key generators.
/// </summary>
/// <typeparam name="TAggregateRootModel">Type of the aggregate root model.</typeparam>
public interface IPassportKeyGenerator<TAggregateRootModel>
    : IAggregateKeyGenerator<TAggregateRootModel, PassportKey>
    where TAggregateRootModel : IAggregateRootModel;
