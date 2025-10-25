// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Implements a secret type for managing passwords.
/// </summary>
public sealed class Password : Secret<Password>
{
    private Password()
    {
    }

    private Password(string encodedValue, DateTimeOffset expireDate, SecretTypes type)
        : base(encodedValue, expireDate, type)
    {
    }
}
