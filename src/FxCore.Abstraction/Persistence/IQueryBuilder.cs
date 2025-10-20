// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a query builder object that creates a query definition by using specifications,
/// paging, and sorting configuration objects.
/// </summary>
public interface IQueryBuilder
{
    /// <summary>
    /// Builds a queryable object.
    /// </summary>
    /// <param name="baseQuery">The source queryable collection of entities.</param>
    /// <param name="specification">The specification to apply to the query.</param>
    /// <param name="pager">The pager to apply to the query.</param>
    /// <typeparam name="TModel">The type of the data model.</typeparam>
    /// <returns>A queryable collection of entities.</returns>
    IQueryable<TModel> Build<TModel>(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager)
        where TModel : class, IDataModel;
}
