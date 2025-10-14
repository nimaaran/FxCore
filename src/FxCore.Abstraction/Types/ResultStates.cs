// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Types;

/// <summary>
/// Defines a list of overall result states for operations.
/// </summary>
public enum ResultStates : byte
{
    /// <summary>
    /// Indicates that the operation has failed due to an unhandled error.
    /// </summary>
    FAILED = 0,

    /// <summary>
    /// Indicated that the operation has been completed successfully.
    /// </summary>
    COMPLETED = 1,

    /// <summary>
    /// Indicates that the operation has been terminated before completion due to a handled
    /// situation.
    /// </summary>
    TERMINATED = 2,
}
