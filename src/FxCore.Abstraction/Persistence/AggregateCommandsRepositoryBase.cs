// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Models;

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// As an abstract class, this class enforces basic CRUD operations for repository service
/// providers.
/// </summary>
/// <typeparam name="TAggregateRoot">
/// The type of entity this repository manages. Must implement IEntity.
/// </typeparam>
/// <typeparam name="TKey">The aggregate key type.</typeparam>
public abstract class AggregateCommandsRepositoryBase<TAggregateRoot, TKey> :
    IRecordCreatorRepository<TAggregateRoot>,
    IRecordRemoverRepository<TAggregateRoot>,
    IRecordUpdaterRepository<TAggregateRoot>,
    IAggregateLoaderRepository<TAggregateRoot, TKey>
    where TAggregateRoot : class, IAggregateRoot
    where TKey : IAggregateKey
{
    /// <inheritdoc/>
    public abstract void Create(TAggregateRoot @object);

    /// <inheritdoc/>
    public abstract void Delete(TAggregateRoot @object);

    /// <inheritdoc/>
    public abstract void Update(TAggregateRoot @object);

    /// <inheritdoc/>
    public abstract Task<TAggregateRoot?> LoadAsync(
        TKey key,
        CancellationToken cancellationToken);
}
