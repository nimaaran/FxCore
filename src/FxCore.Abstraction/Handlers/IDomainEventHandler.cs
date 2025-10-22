// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for event handlers.
/// </summary>
/// <typeparam name="TDomainEvent">Type of the domain event object.</typeparam>
public interface IDomainEventHandler<TDomainEvent> : IEventHandler
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Handles a domain event.
    /// </summary>
    /// <param name="event">A domain event that should be handled.</param>
    void Handle(TDomainEvent @event);
}
