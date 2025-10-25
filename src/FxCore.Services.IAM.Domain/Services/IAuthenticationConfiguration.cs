// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Services.IAM.Domain.Services;

/// <summary>
/// Defines a contract for authentication configuration model.
/// </summary>
public interface IAuthenticationConfiguration
{
    /// <summary>
    /// Gets a value indicating after how many failed login attempts the account gets protected.
    /// </summary>
    byte ProtectionThreshold { get; }

    /// <summary>
    /// Gets a value indicating after how many failed login attempts the account gets suspended.
    /// </summary>
    byte SuspensionThreshold { get; }

    /// <summary>
    /// Gets a value indicating the duration of the account protection.
    /// </summary>
    TimeSpan ProtectionDuration { get; }

    /// <summary>
    /// Gets a value indicating the duration of the account suspension.
    /// </summary>
    TimeSpan SuspensionDuration { get; }

    /// <summary>
    /// Gets a timestamp indicating how much time is valid between first authentication step and
    /// second step in two-factor authentication.
    /// </summary>
    TimeSpan TwoFactorStepsGapDuration { get; }

    /// <summary>
    /// Gets the maximum lifetime of passwords.
    /// </summary>
    TimeSpan PasswordLifetime { get; }

    /// <summary>
    /// Gets the maximum lifetime of passcodes.
    /// </summary>
    TimeSpan PasscodeLifetime { get; }
}
