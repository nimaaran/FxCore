// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

/// <summary>
/// Defines a contract for asynchronous event handlers.
/// </summary>
/// <typeparam name="TDomainEventModel">Type of the domain event model.</typeparam>
public interface IAsyncDomainEventHandler<TDomainEventModel> : IEventHandler
    where TDomainEventModel : IDomainEventModel
{
    /// <summary>
    /// Handles a domain event.
    /// </summary>
    /// <param name="event">A domain event that should be handled.</param>
    /// <param name="cancellationToken">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation.</returns>
    Task HandleAsync(TDomainEventModel @event, CancellationToken cancellationToken);
}
