// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines a base record class for implementing aggregate key types.
/// </summary>
/// <typeparam name="TValue">Type of the aggregate key's actual value.</typeparam>
/// <param name="Value">The actual value of the aggregate key.</param>
public abstract record class AggregateKeyBase<TValue>(TValue Value)
    : IAggregateKey<TValue>
    where TValue : notnull;
