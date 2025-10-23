// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Events.Contracts;

/// <summary>
/// Defines a contract for async domain event handlers.
/// </summary>
/// <typeparam name="TDomainEvent">Type of the domain event.</typeparam>
public interface IAsyncDomainEventHandler<TDomainEvent> : IAsyncEventHandler<TDomainEvent>
    where TDomainEvent : IDomainEvent;
