// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Services;

namespace FxCore.Abstraction.Models;

/// <summary>
/// Defines the base implementation for all domain event models.
/// </summary>
public abstract record class DomainEventBase : IDomainEvent
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventBase"/> class.
    /// </summary>
    /// <param name="trackingKey">The event tracking key.</param>
    /// <param name="timestamp">The event timestamp.</param>
    protected DomainEventBase(string trackingKey, DateTimeOffset timestamp)
    {
        this.TrackingKey = trackingKey;
        this.Timestamp = timestamp;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="DomainEventBase"/> class.
    /// </summary>
    /// <param name="dependenciesProvider">
    /// Provides the required dependencies to initialize domain event object.
    /// </param>
    protected DomainEventBase(IEventDependenciesProvider dependenciesProvider)
    {
        this.TrackingKey = dependenciesProvider.TrackingKeyGenerator.Generate();
        this.Timestamp = dependenciesProvider.DateTimeService.Now();
    }

    /// <inheritdoc/>
    public string TrackingKey { get; }

    /// <inheritdoc/>
    public DateTimeOffset Timestamp { get; }
}
