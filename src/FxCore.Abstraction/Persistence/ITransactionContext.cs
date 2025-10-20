// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Persistence;

/// <summary>
/// Represents a transaction context service provider based on the unity of work pattern.
/// </summary>
public interface ITransactionContext
{
    /// <summary>
    /// Commits the transaction.
    /// </summary>
    /// <param name="token">The cancellation token to monitor for cancellation requests.</param>
    /// <returns>The number of records affected by the commit operation.</returns>
    Task<int> CommitAsync(CancellationToken token);
}
