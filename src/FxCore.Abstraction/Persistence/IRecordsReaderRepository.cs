// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a repository object that can read some records.
/// </summary>
/// <typeparam name="TModel">The type of the data model.</typeparam>
public interface IRecordsReaderRepository<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Reads some record depending on the specified specification.
    /// </summary>
    /// <param name="baseQuery">The base query that will be used to read the records.</param>
    /// <param name="specification">The specification of the intended query.</param>
    /// <param name="pager">The pager of the intended query.</param>
    /// <param name="token">A cancellation token.</param>
    /// <returns>
    /// An list of entity objects; or an empty list if the query doesn't return any records.
    /// </returns>
    Task<List<TModel>> ReadAsync(
        IQueryable<TModel> baseQuery,
        ISpecification<TModel> specification,
        IPager<TModel> pager,
        CancellationToken token);
}
