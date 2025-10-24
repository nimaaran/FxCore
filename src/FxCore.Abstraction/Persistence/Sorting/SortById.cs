// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Persistence.Sorting;

/// <summary>
/// Defines a sorter according to the entity id.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public sealed class SortById<TEntity> : SorterBase<TEntity>
    where TEntity : class, IEntity<TEntity>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SortById{TEntity}"/> class.
    /// </summary>
    /// <param name="ascending">Direction of sorting.</param>
    public SortById(bool ascending = true)
        : base(r => r.Id, ascending)
    {
    }
}
