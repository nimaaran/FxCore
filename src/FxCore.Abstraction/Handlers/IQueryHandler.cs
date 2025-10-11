using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface IQueryHandler<TQueryRequestModel, TOutcome> : IRequestHandler
    where TQueryRequestModel : IQueryRequestModel
{
    IQueryResponseModel<TOutcome> Handle(TQueryRequestModel query);
}
