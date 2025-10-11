using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface IAsyncDomainEventHandler<TDomainEventModel> : IEventHandler
    where TDomainEventModel : IDomainEventModel
{
    Task HandleAsync(TDomainEventModel @event, CancellationToken cancellationToken);
}
