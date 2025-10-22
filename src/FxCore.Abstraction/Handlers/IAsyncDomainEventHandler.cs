// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for the required attributes and behaviors of asynchronous event handlers.
/// </summary>
/// <typeparam name="TDomainEvent">Type of the domain event object.</typeparam>
public interface IAsyncDomainEventHandler<TDomainEvent> : IEventHandler
    where TDomainEvent : IDomainEvent
{
    /// <summary>
    /// Handles a domain event.
    /// </summary>
    /// <param name="event">A domain event object that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation without any outcome.</returns>
    Task HandleAsync(TDomainEvent @event, CancellationToken cancellationToken);
}
