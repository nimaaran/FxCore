// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Common.Models;

/// <summary>
/// Defines a standard model for operation results.
/// </summary>
public sealed class Result
{
    private readonly object? outcome;

    private Result(ResultStates state, Enum code,  string message, object? outcome = null)
    {
        this.State = state;
        this.Code = code;
        this.Message = message;
        this.outcome = outcome;
    }

    /// <summary>
    /// Gets a code that describes what happened. It could be a predefined code in the
    /// <see cref="ResultCodes"/> or other custom codes in other enum types.
    /// </summary>
    public Enum Code { get; }

    /// <summary>
    /// Gets the overall state of the operation. This value just indicates that an operation has
    /// been failed, terminated, or succeed and will be used for making quick decisions.
    /// </summary>
    public ResultStates State { get; }

    /// <summary>
    /// Gets a value indicating whether the operation has generated an outcome object or not.
    /// </summary>
    public bool HasOutcome { get => this.outcome is not null; }

    /// <summary>
    /// Gets an additional and user-friendly message about the operation final state.
    /// </summary>
    public string Message { get; } = string.Empty;

    /// <summary>
    /// Creates a new result object when the opration has completed successfully. This method is
    /// appropriate for those operations that do not generate any outcome.
    /// </summary>
    /// <param name="message">See <see cref="Message"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Completed(string message = "Operation completed.")
       => new(ResultStates.COMPLETED, ResultCodes.NO_CONTENT, message);

    /// <summary>
    /// Creates a new result object when the opration has completed successfully. This method is
    /// appropriate for those operations that generate outcome.
    /// </summary>
    /// <param name="outcome">
    /// The operation outcome. Sometimes, it could be a <see langword="null"/> value.
    /// </param>
    /// <param name="message">See <see cref="Message"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Completed(object? outcome, string message = "Operation completed.")
        => new(ResultStates.COMPLETED, ResultCodes.OK, message, outcome);

    /// <summary>
    /// Creates a new result object when the opration has terminated expectedly.
    /// </summary>
    /// <param name="code">See <see cref="Code"/>.</param>
    /// <param name="message">See <see cref="Message"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Terminated(Enum code, string message = "Operation terminated.")
    {
        return new(ResultStates.TERMINATED, code, message);
    }

    /// <summary>
    /// Creates a new result object when the opration has failed unexpectedly.
    /// </summary>
    /// <param name="message">See <see cref="Message"/>.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Failed(string message = "Operation failed.")
        => new(ResultStates.TERMINATED, ResultCodes.SERVER_ERROR, message);

    /// <summary>
    /// Extracts the outcome object if it is available and of the desired type.
    /// </summary>
    /// <typeparam name="TOutcome">Type of the desired outcome object.</typeparam>
    /// <param name="outcome">The outcome object.</param>
    /// <returns>
    /// <see langword="true"/> if the result object contains an outcome; otherwise, it returns
    /// <see langword="false"/>.
    /// </returns>
    public bool TryGetOutcome<TOutcome>(out TOutcome? outcome)
    {
        if (this.HasOutcome && this.outcome is TOutcome obj)
        {
            outcome = obj;
            return true;
        }

        outcome = default;
        return false;
    }
}
