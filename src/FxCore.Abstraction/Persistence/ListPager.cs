// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a paging configuration object that splits records into different pages and
/// specifies a page to be returned.
/// </summary>
/// <typeparam name="TModel">The type of the query result data model.</typeparam>
public class ListPager<TModel> : IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListPager{TModel}"/> class.
    /// </summary>
    /// <param name="pageSize">The number of records that each page could have.</param>
    /// <param name="pageIndex">The intended page index.</param>
    /// <param name="sorter">
    /// The sorting configuration object that should be applied before paging.
    /// </param>
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
