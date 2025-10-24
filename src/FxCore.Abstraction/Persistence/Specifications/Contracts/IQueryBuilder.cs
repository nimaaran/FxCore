// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Paging.Contracts;

namespace FxCore.Abstraction.Persistence.Specifications.Contracts;

/// <summary>
/// Represents a query builder that makes a query by combinig specifications, pagers, and sorters.
/// </summary>
public interface IQueryBuilder
{
    /// <summary>
    /// Builds a queryable object.
    /// </summary>
    /// <param name="baseQuery">
    /// The base data source that should be used for making the query.
    /// </param>
    /// <param name="specification">The specification to apply to the query.</param>
    /// <param name="pager">The pager to apply to the query.</param>
    /// <typeparam name="TModel">Type of the data model.</typeparam>
    /// <returns>The final query that can be executed to retrieve records.</returns>
    IQueryable<TModel> Build<TModel>(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager)
        where TModel : class, IDataModel;
}
