// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;
using FxCore.Abstraction.Persistence.Specifications.Contracts;

namespace FxCore.Abstraction.Persistence.Repositories.Contracts;

/// <summary>
/// Defines a contract for those repository methods that load just an object.
/// </summary>
/// <typeparam name="TModel">Type of the data model.</typeparam>
public interface IRecordReaderRepository<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Reads a record depending on the specified specification.
    /// </summary>
    /// <param name="baseQuery">The queryable dataset.</param>
    /// <param name="specification">The specification of the intended query.</param>
    /// <param name="sorter">A sorter object.</param>
    /// <param name="cancellationToken">See the <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation that might return an object.</returns>
    Task<TModel?> ReadAsync(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        ISorter<TModel> sorter,
        CancellationToken cancellationToken);
}
