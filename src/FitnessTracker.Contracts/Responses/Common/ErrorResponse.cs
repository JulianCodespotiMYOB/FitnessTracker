namespace FitnessTracker.Contracts.Responses.Common;

public record ErrorResponse
{
    public IEnumerable<string> Errors { get; }

    public ErrorResponse(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public ErrorResponse(string? error)
    {
        Errors = new[] { error ?? "An unknown error has occured." };
    }
}