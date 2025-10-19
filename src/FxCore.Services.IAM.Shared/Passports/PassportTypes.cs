// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Passports;

/// <summary>
/// Defines a list of possible types for a passport.
/// </summary>
public enum PassportTypes : byte
{
    /// <summary>
    /// Indicates that the passport is a basic username and password combination.
    /// </summary>
    BASIC = 1,

    /// <summary>
    /// Indicates that the passport is based on an email address.
    /// </summary>
    EMAIL = 2,

    /// <summary>
    /// Indicates that the passport is based on a phone number.
    /// </summary>
    PHONE = 3,
}
