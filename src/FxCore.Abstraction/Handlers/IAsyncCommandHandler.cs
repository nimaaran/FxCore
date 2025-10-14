// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for asynchronous command requests handlers.
/// </summary>
/// <typeparam name="TCommandRequestModel">Type of the command request object.</typeparam>
public interface IAsyncCommandHandler<TCommandRequestModel> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    /// <summary>
    /// Handles the command request.
    /// </summary>
    /// <param name="command">The command request that should be hanlded.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation.</returns>
    Task HandleAsync(
        TCommandRequestModel command,
        CancellationToken cancellationToken);
}

/// <summary>
/// Defines a contract for asynchronous command requests handlers.
/// </summary>
/// <typeparam name="TCommandRequestModel">Type of the command request object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler outcome.</typeparam>
public interface IAsyncCommandHandler<TCommandRequestModel, TOutcome> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    /// <summary>
    /// Handles the command request and produces an outcome.
    /// </summary>
    /// <param name="command">The command request that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation that productes an outcome object.</returns>
    Task<ICommandResponseModel<TOutcome>> HandleAsync(
        TCommandRequestModel command,
        CancellationToken cancellationToken);
}
