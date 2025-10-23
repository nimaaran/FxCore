// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Requests.Contracts;

/// <summary>
/// Defines a contract for sync command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command.</typeparam>
public interface ICommandHandler<TCommandRequest> : IRequestHandler<TCommandRequest>
    where TCommandRequest : ICommand
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command object that should be handled.</param>
    void Handle(TCommandRequest command);
}

/// <summary>
/// Defines a contract for command handlers.
/// </summary>
/// <typeparam name="TCommandRequest">Type of the command.</typeparam>
/// <typeparam name="TOutcome">Type of the outcome.</typeparam>
public interface ICommandHandler<TCommandRequest, TOutcome> : IRequestHandler<TCommandRequest>
    where TCommandRequest : ICommand
{
    /// <summary>
    /// Handles the command.
    /// </summary>
    /// <param name="command">The command that should be handled.</param>
    /// <returns>
    /// The response of the operation as type of <see cref="ICommandResponse{TOutcome}"/>.
    /// </returns>
    ICommandResponse<TOutcome> Handle(TCommandRequest command);
}
