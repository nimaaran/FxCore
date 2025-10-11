namespace FxCore.Abstraction.Models;

public interface IQueryResponseModel<TOutcome> : IResponseModel
{
    long Total { get; }
    IReadOnlyCollection<TOutcome> Result { get; }
}
