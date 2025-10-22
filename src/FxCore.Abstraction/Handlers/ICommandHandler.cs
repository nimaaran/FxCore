// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// DDefines a contract for the required attributes and behaviors of command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command object.</typeparam>
public interface ICommandHandler<TCommandRequest> : IRequestHandler
    where TCommandRequest : ICommandRequest
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command object that should be handled.</param>
    void Handle(TCommandRequest command);
}

/// <summary>
/// Defines a contract for the required attributes and behaviors of command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command object.</typeparam>
/// <typeparam name="TOutcome">Type of the handler's outcome.</typeparam>
public interface ICommandHandler<TCommandRequest, TOutcome> : IRequestHandler
    where TCommandRequest : ICommandRequest
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command object that should be handled.</param>
    /// <returns>
    /// The response of the operation as type of <see cref="ICommandResponse{TOutcome}"/>.
    /// </returns>
    ICommandResponse<TOutcome> Handle(TCommandRequest command);
}
