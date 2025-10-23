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
/// <typeparam name="TQueryRequest">Type of the query.</typeparam>
/// <typeparam name="TOutcome">Type of the outcome.</typeparam>
public interface IQueryHandler<TQueryRequest, TOutcome> : IRequestHandler<TQueryRequest>
    where TQueryRequest : IQuery
{
    /// <summary>
    /// Handles a query.
    /// </summary>
    /// <param name="query">The query that should be handled.</param>
    /// <returns>
    /// The response of the operation as type of <see cref="IQueryResponse{TOutcome}"/>.
    /// </returns>
    IQueryResponse<TOutcome> Handle(TQueryRequest query);
}
