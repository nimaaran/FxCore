using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface IAsyncQueryHandler<TQueryRequestModel, TOutcome> : IRequestHandler
    where TQueryRequestModel : IQueryRequestModel
{
    Task<IQueryResponseModel<TOutcome>> HandleAsync(TQueryRequestModel query, CancellationToken cancellationToken);
}
