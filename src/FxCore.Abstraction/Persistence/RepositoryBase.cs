// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// As an abstract class, this class enforces basic CRUD operations for repository service
/// providers.
/// </summary>
/// <typeparam name="TEntity">
/// The type of entity this repository manages. Must implement IEntity.
/// </typeparam>
public abstract class RepositoryBase<TEntity> :
    IRecordCreatorRepository<TEntity>,
    IRecordReaderRepository<TEntity>,
    IRecordRemoverRepository<TEntity>,
    IRecordsReaderRepository<TEntity>,
    IRecordUpdaterRepository<TEntity>
    where TEntity : class, IEntityModel
{
    /// <inheritdoc/>
    public abstract void Create(TEntity @object);

    /// <inheritdoc/>
    public abstract Task<TEntity?> ReadAsync(
        IQueryable<TEntity> baseQuery,
        ISpecification<TEntity> specification,
        ISorter<TEntity> sorter,
        CancellationToken token);

    /// <inheritdoc/>
    public abstract Task<List<TEntity>> ReadAsync(
        IQueryable<TEntity> baseQuery,
        ISpecification<TEntity> specification,
        IPager<TEntity> pager,
        CancellationToken token);

    /// <inheritdoc/>
    public abstract void Delete(TEntity @object);

    /// <inheritdoc/>
    public abstract void Update(TEntity @object);
}
