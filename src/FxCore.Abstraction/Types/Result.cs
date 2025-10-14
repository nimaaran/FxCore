// ┌──────────────────────────────────────────────────────────────────────────────────────────────┐
// │ALL RIGHTS RESERVED.                                                                          │
// │THIS FILE IS PART OF FXCORE FRAMEWORK AND DEVELOPED BY NIMA ARAN AND FXCORE CONTRIBUTORS TEAM.│
// │FOR MORE INFORMATION ABOUT FXCORE, PLEASE VISIT HTTPS://GITHUB.COM/NIMAARAN/FXCORE            │
// └──────────────────────────────────────────────────────────────────────────────────────────────┘

namespace FxCore.Abstraction.Types;

/// <summary>
/// Defines the required attributes and behaviors of an object that contains the latest state and
/// final result of an operation.
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
    /// Gets the result code of the operation.
    /// </summary>
    public Enum Code { get; }

    /// <summary>
    /// Gets the overall state of the operation.
    /// </summary>
    public ResultStates State { get; }

    /// <summary>
    /// Gets a value indicating whether the result contains an outcome object or not.
    /// </summary>
    public bool HasOutcome { get => this.outcome is not null; }

    /// <summary>
    /// Gets an additional message about the operation result.
    /// </summary>
    public string Message { get; } = string.Empty;

    /// <summary>
    /// Creates a new result object indicating that the operation has been completed successfully.
    /// </summary>
    /// <param name="message">An optional message.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Completed(string message = "Completed.")
       => new(ResultStates.COMPLETED, ResultCodes.OK, message);

    /// <summary>
    /// Creates a new result object indicating that the operation has been completed successfully.
    /// </summary>
    /// <param name="outcome">The operation outcome.</param>
    /// <param name="message">An optional message.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Completed(object? outcome, string message = "Completed.")
        => new(ResultStates.COMPLETED, ResultCodes.OK, message, outcome);

    /// <summary>
    /// Creates a new result object indicating that the operation has been terminated.
    /// </summary>
    /// <param name="code">A code that provides more information about what happened.</param>
    /// <param name="message">An optional message.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Terminated(Enum code, string message = "Terminated.")
    {
        return new(ResultStates.TERMINATED, code, message);
    }

    /// <summary>
    /// Creates a new result object indicating that the operation has been failed.
    /// </summary>
    /// <param name="message">An optional message.</param>
    /// <returns>An object as type of the <see cref="Result"/>.</returns>
    public static Result Failed(string message = "Failed.")
        => new(ResultStates.TERMINATED, ResultCodes.SERVER_ERROR, message);

    /// <summary>
    ///     Extract the operation outcome object if it is available and of the desired type.
    /// </summary>
    /// <typeparam name="TOutcome">Type of the desired outcome object.</typeparam>
    /// <param name="outcome">The outcome object.</param>
    /// <returns>
    ///     <see langword="true"/> means the result object contains an outcome; otherwise, it returns
    ///     <see langword="false"/>.
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
