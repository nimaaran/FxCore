// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models.Contracts;
using FxCore.Abstraction.Persistence.Paging.Contracts;
using FxCore.Abstraction.Persistence.Sorting.Contracts;

namespace FxCore.Abstraction.Persistence.Paging;

/// <summary>
/// Implements a pager that returns only one record from the top of the list.
/// </summary>
/// <typeparam name="TModel">Type of the query result.</typeparam>
public class OnlyOnePager<TModel> : IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="OnlyOnePager{TModel}"/> class.
    /// </summary>
    /// <param name="sorter">See <see cref="IPager{TModel}.Sorter"/>.</param>
    public OnlyOnePager(ISorter<TModel> sorter)
    {
        this.PageSize = 1;
        this.PageIndex = 0;
        this.Sorter = sorter;
    }

    /// <inheritdoc/>
    public int PageSize { get; init; }

    /// <inheritdoc/>
    public int PageIndex { get; init; }

    /// <inheritdoc/>
    public ISorter<TModel> Sorter { get; init; }
}
