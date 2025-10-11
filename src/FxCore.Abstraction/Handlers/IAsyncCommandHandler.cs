using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface IAsyncCommandHandler<TCommandRequestModel> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    Task HandleAsync(TCommandRequestModel command, CancellationToken cancellationToken);
}

public interface IAsyncCommandHandler<TCommandRequestModel, TOutcome> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    Task<ICommandResponseModel<TOutcome>> HandleAsync(TCommandRequestModel command, CancellationToken cancellationToken);
}
