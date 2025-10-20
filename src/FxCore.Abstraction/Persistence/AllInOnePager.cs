// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a paging configuration object that returns all records in a single page.
/// </summary>
/// <typeparam name="TModel">The type of the query result data model.</typeparam>
public class AllInOnePager<TModel> : IPager<TModel>
    where TModel : class, IDataModel
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AllInOnePager{TModel}"/> class.
    /// </summary>
    /// <param name="sorter">
    /// The sorting configuration object that should be applied before paging.
    /// </param>
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
