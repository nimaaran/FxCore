// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Implements a secret type for managing passcodes and OTPs.
/// </summary>
public sealed class Passcode : Secret<Passcode>
{
    private Passcode()
    {
    }

    private Passcode(string encodedValue, DateTimeOffset expireDate, SecretTypes type)
        : base(encodedValue, expireDate, type)
    {
    }
}
