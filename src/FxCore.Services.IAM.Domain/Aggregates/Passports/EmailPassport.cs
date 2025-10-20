// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

using FxCore.Abstraction.Services;
using FxCore.Abstraction.Types;
using FxCore.Services.IAM.Domain.Services;
using FxCore.Services.IAM.Shared.Accounts;
using FxCore.Services.IAM.Shared.Passports;

namespace FxCore.Services.IAM.Domain.Aggregates.Passports;

/// <summary>
/// Defines an entity model for email passports.
/// </summary>
public sealed class EmailPassport : Passport<EmailPassport>
{
    private EmailPassport()
        : base()
    {
    }

    private EmailPassport(
        IEventDependenciesProvider dependencies,
        IPassportKeyGenerator<EmailPassport> aggregateKeyGenerator,
        AccountKey accountKey,
        string identity,
        out Result result)
        : base(
            dependencies,
            aggregateKeyGenerator,
            accountKey,
            identity,
            PassportTypes.EMAIL,
            PassportStates.PENDING,
            out result)
    {
    }

    /// <summary>
    /// Creates a new passport that will use an email address as the identity for authentication.
    /// </summary>
    /// <param name="dependencies">
    /// An object that provides required dependencies for creating domain events.
    /// </param>
    /// <param name="passportKeyGenerator">A passport key generator.</param>
    /// <param name="accountKey">The relevant account key.</param>
    /// <param name="identity">The passport identity (an email address).</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Register(
        IEventDependenciesProvider dependencies,
        IPassportKeyGenerator<EmailPassport> passportKeyGenerator,
        AccountKey accountKey,
        string identity)
    {
        _ = new EmailPassport(
            dependencies,
            passportKeyGenerator,
            accountKey,
            identity,
            out Result result);

        return result;
    }
}
