// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Entities.Contracts;

namespace FxCore.Abstraction.Persistence.Specifications;

/// <summary>
/// Represents a criterion object that configs the query to return only removed records when
/// the soft-delete removing is available.
/// </summary>
/// <typeparam name="TEntity">Type of the entity.</typeparam>
public sealed class OnlyRemovedRecords<TEntity> : Criterion<TEntity>
    where TEntity : class, IEntity
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OnlyRemovedRecords{TEntity}"/> class.
    /// </summary>
    public OnlyRemovedRecords() => this.Set(r => r.Removed);
}
