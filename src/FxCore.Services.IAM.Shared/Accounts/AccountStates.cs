// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Accounts;

/// <summary>
/// Defines a list of possible states for an account.
/// </summary>
public enum AccountStates : byte
{
    /// <summary>
    /// Indicates that account has been registered but not yet activated.
    /// </summary>
    REGISTERED = 1,

    /// <summary>
    /// Indicates that account is activated and fully functional.
    /// </summary>
    ACTIVATED = 2,

    /// <summary>
    /// Indicates that account is deactivated and not functional.
    /// </summary>
    DEACTIVATED = 3,

    /// <summary>
    /// Indicates that account has restricted access to certain features or services.
    /// </summary>
    RESTRICTED = 4,

    /// <summary>
    /// Indicates that account is permanently closed and cannot be reactivated.
    /// </summary>
    CLOSED = 5,

    /// <summary>
    /// Indicates that account is banned due to violations of terms of service or policies.
    /// </summary>
    BANNED = 6,
}
