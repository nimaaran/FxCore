// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;

namespace FxCore.Abstraction.Events.Contracts;

/// <summary>
/// Defines a marker interface for all kinds of sync event handlers.
/// </summary>
/// <typeparam name="TEvent">Type of the event.</typeparam>
public interface IEventHandler<TEvent> : IMessageHandler<TEvent>
    where TEvent : IEvent
{
    /// <summary>
    /// Handles the event.
    /// </summary>
    /// <param name="event">The event that should be handled.</param>
    void Handle(TEvent @event);
}
