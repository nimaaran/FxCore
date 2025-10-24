// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;
using System.Linq.Expressions;

namespace FxCore.Abstraction.Persistence.Sorting;

/// <summary>
/// As a base class, it defines the required attributes and behaviors of query result sorters.
/// </summary>
/// <typeparam name="TModel">The of the query result.</typeparam>
public abstract class SorterBase<TModel> : ISorter<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="SorterBase{TModel}"/> class.
    /// </summary>
    /// <param name="column">See <see cref="ISorter{TModel}.Column"/>.</param>
    /// <param name="ascending">See <see cref="ISorter{TModel}.Ascending"/>.</param>
    /// <param name="next">See <see cref="ISorter{TModel}.Next"/>.</param>
    protected SorterBase(
        Expression<Func<TModel, object>> column,
        bool ascending = true,
        ISorter<TModel>? next = null)
    {
        this.Column = column;
        this.Ascending = ascending;
        this.Next = next;
    }

    /// <inheritdoc/>
    public bool Ascending { get; }

    /// <inheritdoc/>
    public Expression<Func<TModel, object>> Column { get; }

    /// <inheritdoc/>
    public ISorter<TModel>? Next { get; private set; }

    /// <inheritdoc/>
    public ISorter<TModel> ThenBy(ISorter<TModel> next)
    {
        this.Next = next;
        return this;
    }
}
