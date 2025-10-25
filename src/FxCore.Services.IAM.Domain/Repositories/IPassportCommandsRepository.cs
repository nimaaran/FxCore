// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence.Repositories.Contracts;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Repositories;

/// <summary>
/// Defines a contract to define command methods of passport aggregate repositories.
/// </summary>
/// <typeparam name="TPassport">Type of the passport.</typeparam>
public interface IPassportCommandsRepository<TPassport> :
    IAggregateCommandsRepository<TPassport>
    where TPassport : Passport<TPassport>;
