namespace FxCore.Abstraction.Models;

public interface ICommandResponseModel<TOutcome> : IResponseModel
{
    TOutcome? Result { get; }
}
