// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;

namespace FxCore.Abstraction.Persistence.Specifications;

/// <summary>
/// Defines a specification for loading an active aggregate by its key.
/// </summary>
/// <typeparam name="TRoot">Type of the aggregate.</typeparam>
/// <typeparam name="TKey">The aggregate key type.</typeparam>
public class LoadByKeySpecification<TRoot, TKey> : SpecificationBase<TRoot>
    where TRoot : class, IAggregateRoot<TKey>
    where TKey : notnull, IAggregateKey
{
    /// <summary>
    /// Initializes a new instance of the <see cref="LoadByKeySpecification{TRoot,TKey}"/>
    /// class.
    /// </summary>
    /// <param name="key">The desired aggregate's key.</param>
    public LoadByKeySpecification(TKey key)
    {
        this.Criterion = new Criterion<TRoot>()
            .Set(r => r.Key.Equals(key))
            .And(new OnlyActiveRecords<TRoot>());
    }
}
