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
/// Implements a pager that returns all records in a single page.
/// </summary>
/// <typeparam name="TModel">Type of the query result.</typeparam>
public class AllInOnePager<TModel> : IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllInOnePager{TModel}"/> class.
    /// </summary>
    /// <param name="sorter">See <see cref="IPager{TModel}.Sorter"/>.</param>
    public AllInOnePager(ISorter<TModel> sorter)
    {
        this.PageSize = 0;
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
