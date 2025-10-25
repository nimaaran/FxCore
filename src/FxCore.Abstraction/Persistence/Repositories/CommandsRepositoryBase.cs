// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;
using FxCore.Abstraction.Persistence.DataContexts.Contracts;
using FxCore.Abstraction.Persistence.Paging;
using FxCore.Abstraction.Persistence.Repositories.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories;

/// <summary>
/// Implements a generic base class for command-specific repositories.
/// </summary>
/// <param name="dataContext">A data context that provides access to the data source.</param>
/// <param name="queryBuilder">A query builder object for loading a record.</param>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public abstract class CommandsRepositoryBase<TEntity>(
    IDataContext dataContext,
    IQueryBuilder queryBuilder) :
    IRecordCreatorRepository<TEntity>,
    IRecordReaderRepository<TEntity>,
    IRecordUpdaterRepository<TEntity>,
    IRecordRemoverRepository<TEntity>
    where TEntity : class, IEntity
{
    /// <inheritdoc/>
    public void Create(TEntity @object)
        => dataContext.Create(@object);

    /// <inheritdoc/>
    public void Update(TEntity @object)
        => dataContext.Update(@object);

    /// <inheritdoc/>
    public void Delete(TEntity @object)
        => dataContext.Delete(@object);

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
}
