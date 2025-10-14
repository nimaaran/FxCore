// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Roles;

/// <summary>
/// Defines a list of possible types for a role.
/// </summary>
public enum RoleTypes : byte
{
    /// <summary>
    /// Indicates that the role is defined by users and can be modified or deleted by them.
    /// </summary>
    USER_DEFINED = 1,

    /// <summary>
    /// Indicates that the role is defined by the system and cannot be modified or deleted by
    /// users.
    /// </summary>
    SYSTEM_DEFINED = 2,
}
