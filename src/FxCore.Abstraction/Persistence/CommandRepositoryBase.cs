// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
///     Implements a generic base class for command repositories.
/// </summary>
/// <param name="dataContext">A data context service provider.</param>
/// <param name="queryBuilder">A query builder object.</param>
/// <typeparam name="TEntity">The type of the entity.</typeparam>
public abstract class CommandRepositoryBase<TEntity>(
    IDataContext dataContext,
    IQueryBuilder queryBuilder) :
    IRecordCreatorRepository<TEntity>,
    IRecordReaderRepository<TEntity>,
    IRecordUpdaterRepository<TEntity>,
    IRecordRemoverRepository<TEntity>
    where TEntity : class, IEntityModel
{
    /// <inheritdoc/>
    public void Create(TEntity @object)
        => dataContext.Create<TEntity>(@object);

    /// <inheritdoc/>
    public async Task<TEntity?> ReadAsync(
        IQueryable<TEntity> baseQuery,
        ISpecification<TEntity> specification,
        ISorter<TEntity> sorter,
        CancellationToken token)
    {
        var pager = new OnlyOnePager<TEntity>(sorter);
        var query = queryBuilder.Build(baseQuery, specification, pager);
        var result = await dataContext.ReadAsync(query, token);

        return result.FirstOrDefault();
    }

    /// <inheritdoc/>
    public void Update(TEntity @object)
        => dataContext.Update<TEntity>(@object);

    /// <inheritdoc/>
    public void Delete(TEntity @object)
        => dataContext.Delete<TEntity>(@object);
}
