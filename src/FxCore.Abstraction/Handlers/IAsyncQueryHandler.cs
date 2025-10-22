// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for the required attributes and behaviors of asynchronous query handlers.
/// </summary>
/// <typeparam name="TQueryRequest">Type of the query object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler's outcome.</typeparam>
public interface IAsyncQueryHandler<TQueryRequest, TOutcome> : IRequestHandler
    where TQueryRequest : IQueryRequest
{
    /// <summary>
    /// Handles a query.
    /// </summary>
    /// <param name="query">The query object that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// An async operation that productes a response as type of
    /// <see cref="IQueryResponse{TOutcome}"/>.
    /// </returns>
    Task<IQueryResponse<TOutcome>> HandleAsync(
        TQueryRequest query,
        CancellationToken cancellationToken);
}
