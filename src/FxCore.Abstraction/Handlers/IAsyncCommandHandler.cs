// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for the required attributes and behaviors of asynchronous command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command object.</typeparam>
public interface IAsyncCommandHandler<TCommandRequest> : IRequestHandler
    where TCommandRequest : ICommandRequest
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command object that should be hanlded.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation without any outcome.</returns>
    Task HandleAsync(
        TCommandRequest command,
        CancellationToken cancellationToken);
}

/// <summary>
/// Defines a contract for the required attributes and behaviors of asynchronous command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler's outcome.</typeparam>
public interface IAsyncCommandHandler<TCommandRequest, TOutcome> : IRequestHandler
    where TCommandRequest : ICommandRequest
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command object that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>
    /// An async operation that productes a response as type of
    /// <see cref="ICommandResponse{TOutcome}"/>.
    /// </returns>
    Task<ICommandResponse<TOutcome>> HandleAsync(
        TCommandRequest command,
        CancellationToken cancellationToken);
}
