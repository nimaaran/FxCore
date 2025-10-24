// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Persistence.Specifications;

/// <summary>
/// Defines different operators that could be used to combine criteria.
/// </summary>
public enum CriteriaOperators : byte
{
    /// <summary>
    /// The OR operator.
    /// </summary>
    OR = 1,

    /// <summary>
    /// The AND operator.
    /// </summary>
    AND = 2,
}
