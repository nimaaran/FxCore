// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Common.Models;
using FxCore.Abstraction.Events.Contracts;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines an entity model for basic passports.
/// </summary>
public sealed class BasicPassport : Passport<BasicPassport>
{
    private BasicPassport()
        : base()
    {
    }

    private BasicPassport(
        IEventDependenciesProvider dependencies,
        IPassportKeyGenerator<BasicPassport> aggregateKeyGenerator,
        AccountKey accountKey,
        string identity,
        out Result result)
        : base(
            dependencies,
            aggregateKeyGenerator,
            accountKey,
            identity,
            PassportTypes.BASIC,
            PassportStates.PENDING,
            out result)
    {
    }

    /// <summary>
    /// Creates a new passport that will use a username as the identity for authentication.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="passportKeyGenerator">A passport key generator.</param>
    /// <param name="accountKey">The relevant account key.</param>
    /// <param name="identity">The passport identity.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IEventDependenciesProvider dependencies,
        IPassportKeyGenerator<BasicPassport> passportKeyGenerator,
        AccountKey accountKey,
        string identity)
    {
        _ = new BasicPassport(
            dependencies,
            passportKeyGenerator,
            accountKey,
            identity,
            out Result result);

        return result;
    }
}
