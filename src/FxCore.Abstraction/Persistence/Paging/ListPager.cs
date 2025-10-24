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
/// Implements a pager that splits records into different pages and return only records of a
/// specified page.
/// </summary>
/// <typeparam name="TModel">The type of the query result data model.</typeparam>
public class ListPager<TModel> : IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListPager{TModel}"/> class.
    /// </summary>
    /// <param name="pageSize">See <see cref="IPager{TModel}.PageSize"/>.</param>
    /// <param name="pageIndex">See <see cref="IPager{TModel}.PageIndex"/>.</param>
    /// <param name="sorter">See <see cref="IPager{TModel}.Sorter"/>.</param>
    public ListPager(int pageSize, int pageIndex, ISorter<TModel> sorter)
    {
        this.PageSize = pageSize;
        this.PageIndex = pageIndex;
        this.Sorter = sorter;
    }

    /// <inheritdoc/>
    public int PageSize { get; init; }

    /// <inheritdoc/>
    public int PageIndex { get; init; }

    /// <inheritdoc/>
    public ISorter<TModel> Sorter { get; init; }
}
