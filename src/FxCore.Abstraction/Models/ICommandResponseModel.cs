// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a contract for defining command response models.
/// </summary>
/// <typeparam name="TOutcome">Type of the response model.</typeparam>
public interface ICommandResponseModel<TOutcome> : IResponseModel
{
    /// <summary>
    /// Gets the response object.
    /// </summary>
    TOutcome? Result { get; }
}
