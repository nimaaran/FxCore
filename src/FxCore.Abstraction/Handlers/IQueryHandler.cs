// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for query requests handlers.
/// </summary>
/// <typeparam name="TQueryRequestModel">Type of the query request object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler outcome.</typeparam>
public interface IQueryHandler<TQueryRequestModel, TOutcome> : IRequestHandler
    where TQueryRequestModel : IQueryRequestModel
{
    /// <summary>
    /// Handles a query request and produces an outcome.
    /// </summary>
    /// <param name="query">The query request that should be handled.</param>
    /// <returns>The outcome of the operation.</returns>
    IQueryResponseModel<TOutcome> Handle(TQueryRequestModel query);
}
