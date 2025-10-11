using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Handlers;

public interface ICommandHandler<TCommandRequestModel> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    void Handle(TCommandRequestModel command);
}

public interface ICommandHandler<TCommandRequestModel, TOutcome> : IRequestHandler
    where TCommandRequestModel : ICommandRequestModel
{
    ICommandResponseModel<TOutcome> Handle(TCommandRequestModel command);
}
