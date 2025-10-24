// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Shared.Roles;

/// <summary>
/// Defines a list of possible states for a role.
/// </summary>
public enum RoleStates : byte
{
    /// <summary>
    /// Indicates that the role is enabled and can be used to authorize users.
    /// </summary>
    ENABLED = 1,

    /// <summary>
    /// Indicates that the role is disabled and cannot be used to authorize users.
    /// </summary>
    DISABLED = 2,
}
