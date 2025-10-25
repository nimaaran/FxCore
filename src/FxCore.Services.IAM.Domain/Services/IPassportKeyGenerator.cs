// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a generic contract for passport key generators.
/// </summary>
/// <typeparam name="TPassport">Type of the passport.</typeparam>
public interface IPassportKeyGenerator<TPassport> : IAggregateKeyGenerator<TPassport, PassportKey>
    where TPassport : Passport<TPassport>;
