// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Events.Contracts;

namespace FxCore.Abstraction.Events;

/// <summary>
/// As a base class, it will be used for implementing all domain events.
/// </summary>
public abstract record class DomainEventBase : EventBase, IDomainEvent
{
    /// <inheritdoc/>
    protected DomainEventBase(IEventDependenciesProvider dependencies)
        : base(dependencies)
    {
    }

    /// <inheritdoc/>
    protected DomainEventBase(string trackingKey, DateTimeOffset timestamp)
        : base(trackingKey, timestamp)
    {
    }
}
