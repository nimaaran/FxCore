// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Accounts;

/// <summary>
/// Defines a list of possible types for an account.
/// </summary>
public enum AccountTypes : byte
{
    /// <summary>
    /// A regular user account.
    /// </summary>
    USER = 1,

    /// <summary>
    /// A guest account with limited access for anonymous users.
    /// </summary>
    GUEST = 2,

    /// <summary>
    /// A system account used for internal operations.
    /// </summary>
    SYSTEM = 3,

    /// <summary>
    /// A machine or service account used for automated processes or external services.
    /// </summary>
    MACHINE = 4,
}
