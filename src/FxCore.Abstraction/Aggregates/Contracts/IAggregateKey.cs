// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Aggregates.Contracts;

/// <summary>
/// Defines a marker interface for all kinds of aggregate keys.
/// </summary>
public interface IAggregateKey;

/// <summary>
/// Defines a marker interface for all kinds of aggregate keys.
/// </summary>
/// <typeparam name="TValue">Type of the actual value of the aggregate key.</typeparam>
public interface IAggregateKey<out TValue> : IAggregateKey
    where TValue : notnull
{
    /// <summary>
    /// Gets the actual value of the aggregate key.
    /// </summary>
    TValue Value { get; }
}
