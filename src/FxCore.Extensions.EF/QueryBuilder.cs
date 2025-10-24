// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Paging.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Extensions.EF;

/// <summary>
/// Implements a query builder for EF.
/// </summary>
public sealed class QueryBuilder : IQueryBuilder
{
    /// <inheritdoc/>
    public IQueryable<TModel> Build<TModel>(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager)
        where TModel : class, IDataModel
    {
        IQueryable<TModel> SetSorter(IQueryable<TModel> query, ISorter<TModel> sorter)
        {
            if (sorter.Ascending)
            {
                query = query.OrderBy(sorter.Column);
            }
            else
            {
                query = query.OrderByDescending(sorter.Column);
            }

            if (sorter.Next is not null)
            {
                query = SetSorter(query, sorter.Next);
            }

            return query;
        }

        var criterion = specification.Criterion!.Export();

        var query = baseQuery.Where(criterion);

        query = SetSorter(query, pager.Sorter);
        query = query.Skip(pager.PageSize * pager.PageIndex);
        query = query.Take(pager.PageSize);

        return query;
    }
}
