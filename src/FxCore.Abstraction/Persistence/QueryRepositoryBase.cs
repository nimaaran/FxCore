// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Implements a generic base class for query repositories.
/// </summary>
/// <param name="dataContext">A data context service provider.</param>
/// <param name="queryBuilder">A query builder object.</param>
/// <typeparam name="TModel">The type of the data model.</typeparam>
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
