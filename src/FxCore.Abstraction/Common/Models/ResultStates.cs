// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Models;

/// <summary>
/// Defines a list of overall result states for operations.
/// </summary>
public enum ResultStates : byte
{
    /// <summary>
    /// The operation failed due to an unexpected error.
    /// </summary>
    FAILED = 0,

    /// <summary>
    /// The operation has been completed successfully.
    /// </summary>
    COMPLETED = 1,

    /// <summary>
    /// The operation has been terminated due to an expected and handled error.
    /// </summary>
    TERMINATED = 2,
}
