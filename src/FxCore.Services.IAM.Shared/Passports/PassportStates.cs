// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Passports;

/// <summary>
/// Defines a list of possible states for a passport.
/// </summary>
public enum PassportStates : byte
{
    /// <summary>
    /// Indicates that the passport is created but not yet activated.
    /// </summary>
    PENDING = 0,

    /// <summary>
    /// Indicates that the passport is activated and can be used for authentication.
    /// </summary>
    ACTIVATED = 1,

    /// <summary>
    /// Indicates that the passport is deactivated and cannot be used for authentication.
    /// Reactivation required.
    /// </summary>
    DEACTIVATED = 2,
}
