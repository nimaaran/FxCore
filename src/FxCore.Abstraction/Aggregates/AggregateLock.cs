// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Aggregates;

/// <summary>
/// Defines the required attributes and behaviors of aggregate locks.
/// </summary>
/// <param name="Version">The version of the object.</param>
/// <param name="Timestamp">The latest change date and time.</param>
public record class AggregateLock(int Version, DateTimeOffset Timestamp)
{
    /// <summary>
    /// Gets an empty instance of <see cref="AggregateLock"/> with default values.
    /// </summary>
    public static AggregateLock Empty
        => new(Version: default, Timestamp: DateTimeOffset.MinValue);

    /// <summary>
    /// Creates a new aggregate lock instance.
    /// </summary>
    /// <param name="timestamp">The object creation's date and time.</param>
    /// <returns>Returs the generated aggregate lock object.</returns>
    public static AggregateLock Create(DateTimeOffset timestamp)
        => new(Version: default, Timestamp: timestamp);

    /// <summary>
    /// Creates a new aggregate lock by incrementing the aggregate version by the specified count.
    /// </summary>
    /// <param name="count">Number of applied changes in the aggregate.</param>
    /// <param name="timestamp">The last change's date and time.</param>
    /// <returns>Returns an new aggregate lock with updated values.</returns>
    public AggregateLock Update(int count, DateTimeOffset timestamp)
        => new(Version: this.Version + count, Timestamp: timestamp);
}
