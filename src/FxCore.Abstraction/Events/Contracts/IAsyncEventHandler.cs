// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Events.Contracts;

/// <summary>
/// Defines a marker interface for all kinds of async event handlers.
/// </summary>
/// <typeparam name="TEvent">Type of the event.</typeparam>
public interface IAsyncEventHandler<TEvent> : IAsyncMessageHandler<TEvent>
    where TEvent : IEvent
{
    /// <summary>
    /// Handles the event.
    /// </summary>
    /// <param name="event">The event that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation that does not produce any outcome.</returns>
    Task HandleAsync(TEvent @event, CancellationToken cancellationToken);
}
