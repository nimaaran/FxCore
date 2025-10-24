// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.DataContexts.Contracts;
using FxCore.Abstraction.Persistence.Paging;
using FxCore.Abstraction.Persistence.Paging.Contracts;
using FxCore.Abstraction.Persistence.Repositories.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories;

/// <summary>
/// Implements a generic base class for query-specific repositories.
/// </summary>
/// <param name="dataContext">A data context that provides access to the data source.</param>
/// <param name="queryBuilder">A query builder object for loading records.</param>
/// <typeparam name="TModel">Type of the data model.</typeparam>
public abstract class QueryRepositoryBase<TModel>(
    IDataContext dataContext,
    IQueryBuilder queryBuilder) :
    IRecordReaderRepository<TModel>,
    IRecordsReaderRepository<TModel>
    where TModel : class, IDataModel
{
    /// <inheritdoc/>
    public async Task<TModel?> ReadAsync(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        ISorter<TModel> sorter,
        CancellationToken token)
    {
        var pager = new OnlyOnePager<TModel>(sorter);
        var query = queryBuilder.Build(baseQuery, specification, pager);
        var result = await dataContext.ReadAsync(query, token);

        return result.FirstOrDefault();
    }

    /// <inheritdoc/>
    public Task<List<TModel>> ReadAsync(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager,
        CancellationToken token)
    {
        var query = queryBuilder.Build(baseQuery, specification, pager);
        return dataContext.ReadAsync(query, token);
    }
}
