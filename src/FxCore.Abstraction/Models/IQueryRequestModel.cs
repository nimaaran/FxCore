namespace FxCore.Abstraction.Models;

public interface IQueryRequestModel : IRequestModel
{
    int Skip { get; }
    int Take { get; }
}
