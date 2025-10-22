// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// As an abstract class, this class enforces basic query operations for repository service
/// providers.
/// </summary>
/// <typeparam name="TAggregateRoot">
/// The type of entity this repository manages. Must implement IEntity.
/// </typeparam>
/// <typeparam name="TKey">The aggregate key type.</typeparam>
public abstract class AggregateQueriesRepositoryBase<TAggregateRoot, TKey> :
    IRecordReaderRepository<TAggregateRoot>,
    IRecordsReaderRepository<TAggregateRoot>
    where TAggregateRoot : class, IAggregateRoot
    where TKey : IAggregateKey
{
    /// <inheritdoc/>
    public abstract Task<TAggregateRoot?> ReadAsync(
        IQueryable<TAggregateRoot> baseQuery,
        ISpecification<TAggregateRoot> specification,
        ISorter<TAggregateRoot> sorter,
        CancellationToken token);

    /// <inheritdoc/>
    public abstract Task<List<TAggregateRoot>> ReadAsync(
        IQueryable<TAggregateRoot> baseQuery,
        ISpecification<TAggregateRoot> specification,
        IPager<TAggregateRoot> pager,
        CancellationToken token);
}
