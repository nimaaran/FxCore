// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Requests.Contracts;

/// <summary>
/// Defines a contract for defining command responses.
/// </summary>
/// <typeparam name="TOutcome">Type of the outcome.</typeparam>
public interface ICommandResponse<TOutcome> : IResponse
{
    /// <summary>
    /// Gets the response object.
    /// </summary>
    TOutcome? Result { get; }
}
