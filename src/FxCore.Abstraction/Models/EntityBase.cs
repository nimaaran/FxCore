using FxCore.Abstraction.Types;

namespace FxCore.Abstraction.Models;

public abstract class EntityBase<TId>(TId id, bool removed) : IEntityModel<TId>
where TId : notnull
{
    public TId Id { get; private set; } = id;

    public bool Removed { get; private set; } = removed;

    protected virtual Result Remove()
    {
        if (this.Removed)
        {
            return Result.Terminated(ResultCodes.NOT_MODIFIED);
        }

        this.Removed = true;

        return Result.Completed();
    }
}
