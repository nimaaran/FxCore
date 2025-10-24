// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates;

namespace FxCore.Services.IAM.Shared.Passports;

/// <summary>
/// Defines the passports aggregate key.
/// </summary>
/// <param name="Value"><inheritdoc/></param>
public record class PassportKey(string Value) : AggregateKeyBase<string>(Value);
