// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence;
using FxCore.Services.IAM.Domain.Aggregates.Passports;

namespace FxCore.Services.IAM.Domain.Repositories;

/// <summary>
/// Defines a contract for passport aggregate repository.
/// </summary>
public interface IPassportRepository :
    IRecordCreatorRepository<IPassport>,
    IRecordUpdaterRepository<IPassport>,
    IRecordRemoverRepository<IPassport>,
    IRecordReaderRepository<IPassport>,
    IRecordsReaderRepository<IPassport>;
