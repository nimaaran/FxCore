// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Requests.Contracts;

/// <summary>
/// Defines a contract for sync query handlers.
/// </summary>
/// <typeparam name="TQuery">Type of the query.</typeparam>
/// <typeparam name="TOutcome">Type of the outcome.</typeparam>
public interface IAsyncQueryHandler<TQuery, TOutcome> : IRequestHandler<TQuery>
    where TQuery : IQuery
{
    /// <summary>
    /// Handles a query.
    /// </summary>
    /// <param name="query">The query that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// An async operation produces an object as type of <see cref="IQueryResponse{TOutcome}"/>.
    /// </returns>
    Task<IQueryResponse<TOutcome>> HandleAsync(TQuery query, CancellationToken cancellationToken);
}
