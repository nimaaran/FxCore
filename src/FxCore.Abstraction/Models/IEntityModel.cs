namespace FxCore.Abstraction.Models;

public interface IEntityModel : IDataModel
{
    bool Removed { get; }
}

public interface IEntityModel<TId> : IEntityModel
    where TId : notnull
{
    TId Id { get; }
}
