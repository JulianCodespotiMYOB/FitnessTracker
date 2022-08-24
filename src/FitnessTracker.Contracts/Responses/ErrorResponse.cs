namespace FitnessTracker.Contracts.Responses;

public record ErrorResponse
{
    public ErrorResponse(IEnumerable<string> errors)
    {
        Errors = errors;
    }

    public ErrorResponse(string? error)
    {
        Errors = new[] { error ?? "An unknown error has occured." };
    }

    public IEnumerable<string> Errors { get; init; }
}