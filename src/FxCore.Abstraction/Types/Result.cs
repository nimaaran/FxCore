namespace FxCore.Abstraction.Types;

public sealed class Result
{
    private readonly object? outcome;

    private Result(ResultStates state, ResultCodes code,  string message, object? outcome = null)
    {
        this.State = state;
        this.Code = code;
        this.Message = message;
        this.outcome = outcome;
    }

    public ResultCodes Code { get; }
    public ResultStates State { get; }
    public bool HasOutcome { get => this.outcome is not null; }
    public string Message { get; } = string.Empty;

    public static Result Completed(string message = "Completed.")
       => new(ResultStates.COMPLETED, ResultCodes.OK, message);

    public static Result Completed(object? outcome, string message = "Completed.")
        => new(ResultStates.COMPLETED, ResultCodes.OK, message, outcome);

    public static Result Terminated(ResultCodes code, string message = "Terminated.")
    {
        if (code is ResultCodes.OK or ResultCodes.SERVER_ERROR)
        {
            throw new ArgumentException("Invalid result code is set.", nameof(code));
        }

        return new(ResultStates.TERMINATED, code, message);
    }

    public static Result Failed(string message = "Failed.")
        => new(ResultStates.TERMINATED, ResultCodes.SERVER_ERROR, message);

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
