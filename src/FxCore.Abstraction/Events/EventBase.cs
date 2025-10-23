// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events.Contracts;

namespace FxCore.Abstraction.Events;

/// <summary>
/// As a base class, it will be used for implementing all events.
/// </summary>
public abstract record class EventBase : IEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="EventBase"/> class.
    /// </summary>
    /// <param name="trackingKey">The event tracking key.</param>
    /// <param name="timestamp">The event timestamp.</param>
    protected EventBase(string trackingKey, DateTimeOffset timestamp)
    {
        this.TrackingKey = trackingKey;
        this.Timestamp = timestamp;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="EventBase"/> class.
    /// </summary>
    /// <param name="dependencies">
    /// Provides the required dependencies to initialize domain event object.
    /// </param>
    protected EventBase(IEventDependenciesProvider dependencies)
    {
        this.TrackingKey = dependencies.TrackingKeyGenerator.Generate();
        this.Timestamp = dependencies.DateTimeService.UtcNow();
    }

    /// <inheritdoc/>
    public string TrackingKey { get; }

    /// <inheritdoc/>
    public DateTimeOffset Timestamp { get; }
}
