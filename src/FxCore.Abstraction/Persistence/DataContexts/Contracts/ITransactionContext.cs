// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Persistence.DataContexts.Contracts;

/// <summary>
/// Desines a contract for transaction managers according to the unity-of-work pattern.
/// </summary>
public interface ITransactionContext
{
    /// <summary>
    /// Commits the transaction.
    /// </summary>
    /// <param name="token">See <see cref="CancellationToken"/>.</param>
    /// <returns>An async operation that returns number of affected objects.</returns>
    Task<int> CommitAsync(CancellationToken token);
}
