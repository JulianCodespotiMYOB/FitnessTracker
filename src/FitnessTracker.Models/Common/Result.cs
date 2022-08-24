using System.Diagnostics.CodeAnalysis;

namespace FitnessTracker.Models.Common;

public class Result<T>
{
    public T? Value { get; set; }
    public string? Error { get; set; }

    [MemberNotNullWhen(true, nameof(Value))]
    public bool IsSuccess => Value is not null;

    public static Result<T> Success(T value)
    {
        return new Result<T> { Value = value };
    }

    public static Result<T> Failure(string? error)
    {
        return new Result<T> { Error = error };
    }
}