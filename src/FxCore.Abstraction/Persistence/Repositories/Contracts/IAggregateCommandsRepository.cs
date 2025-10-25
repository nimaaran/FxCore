// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories.Contracts;

/// <summary>
/// Defines a contract for commands repository of aggregates.
/// </summary>
/// <typeparam name="TRoot">Type of the aggregate root.</typeparam>
public interface IAggregateCommandsRepository<TRoot> :
    IRecordCreatorRepository<TRoot>,
    IRecordReaderRepository<TRoot>,
    IRecordUpdaterRepository<TRoot>,
    IRecordRemoverRepository<TRoot>
    where TRoot : class, IAggregateRoot;
