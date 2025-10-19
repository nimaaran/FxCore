// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Passports;

/// <summary>
/// Defines a list of possible types for a secret.
/// </summary>
public enum SecretTypes : byte
{
    /// <summary>
    /// Indicates that the secret is a password.
    /// </summary>
    PASSWORD = 1,

    /// <summary>
    /// Indicates that the secret is a passcode like an OTP.
    /// </summary>
    PASSCODE = 2,
}
