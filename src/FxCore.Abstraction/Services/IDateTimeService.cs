namespace FxCore.Abstraction.Services;

public interface IDateTimeService : IService
{
    DateTimeOffset Now();
}
