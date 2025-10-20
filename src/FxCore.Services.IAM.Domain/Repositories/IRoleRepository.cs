// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Persistence;
using FxCore.Services.IAM.Domain.Aggregates.Roles;

namespace FxCore.Services.IAM.Domain.Repositories;

/// <summary>
/// Defines a contract for role aggregate repository.
/// </summary>
public interface IRoleRepository :
    IRecordCreatorRepository<Role>,
    IRecordUpdaterRepository<Role>,
    IRecordRemoverRepository<Role>,
    IRecordReaderRepository<Role>,
    IRecordsReaderRepository<Role>;
