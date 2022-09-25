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

    public Result<TT> Map<TT>(Func<T, TT> map)
    {
        return IsSuccess ? Result<TT>.Success(map(Value!)) : Result<TT>.Failure(Error);
    }

    public TT Match<TT>(Func<T, TT> success, Func<string?, TT> failure)
    {
        return IsSuccess ? success(Value!) : failure(Error);
    }
}