// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Aggregates.Contracts;
using FxCore.Abstraction.Persistence.Specifications;
using FxCore.Services.IAM.Shared.Accounts;

namespace FxCore.Services.IAM.Domain.Specifications.Accounts;

/// <summary>
/// Defines a specification for loading account aggregate roots by their keys.
/// </summary>
/// <typeparam name="TAccount">Type of the account.</typeparam>
/// <typeparam name="TKey">Type of the aggregate key.</typeparam>
public class LoadAccountByKeySpecification<TAccount, TKey> : SpecificationBase<TAccount>
    where TAccount : class, IAggregateRoot<TAccount, TKey>
    where TKey : IAggregateKey
{
    /// <summary>
    /// Initializes a new instance of the
    /// <see cref="LoadAccountByKeySpecification{TAccount,TKey}"/> class.
    /// </summary>
    /// <param name="accountKey">The aggregate key of the desired account.</param>
    public LoadAccountByKeySpecification(AccountKey accountKey)
    {
        this.Criterion = new Criterion<TAccount>().Set(r => r.Key.Equals(accountKey))
                                                  .And(new OnlyActiveRecords<TAccount>());
    }
}
