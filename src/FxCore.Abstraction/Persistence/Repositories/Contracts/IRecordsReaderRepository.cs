// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Paging.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories.Contracts;

/// <summary>
/// Defines a contract for those repository methods that load a list of objects.
/// </summary>
/// <typeparam name="TModel">Type of the data model.</typeparam>
public interface IRecordsReaderRepository<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Reads a record depending on the specified specification.
    /// </summary>
    /// <param name="baseQuery">The queryable dataset.</param>
    /// <param name="specification">The specification of the intended query.</param>
    /// <param name="pager">A pager object.</param>
    /// <param name="cancellationToken">See the <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation that returns a list objects.</returns>
    Task<List<TModel>> ReadAsync(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager,
        CancellationToken cancellationToken);
}
