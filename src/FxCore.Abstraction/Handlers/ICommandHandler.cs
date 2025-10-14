// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for command requests handlers.
/// </summary>
/// <typeparam name="TCommandRequestModel">Type of the command request object.</typeparam>
public interface ICommandHandler<TCommandRequestModel> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    /// <summary>
    /// Handles the command request without producing any outcomes.
    /// </summary>
    /// <param name="command">The command request that should be handled.</param>
    void Handle(TCommandRequestModel command);
}

/// <summary>
/// Defines a contract for command requests handlers.
/// </summary>
/// <typeparam name="TCommandRequestModel">Type of the command request object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler outcome.</typeparam>
public interface ICommandHandler<TCommandRequestModel, TOutcome> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    /// <summary>
    /// Handles the command request and produces an outcome.
    /// </summary>
    /// <param name="command">The command request that should be handled.</param>
    /// <returns>The outcome of the operation.</returns>
    ICommandResponseModel<TOutcome> Handle(TCommandRequestModel command);
}
