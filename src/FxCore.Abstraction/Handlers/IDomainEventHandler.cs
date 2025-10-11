using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface IDomainEventHandler<TDomainEventModel> : IEventHandler
    where TDomainEventModel : IDomainEventModel
{
    void Handle(TDomainEventModel @event);
}
