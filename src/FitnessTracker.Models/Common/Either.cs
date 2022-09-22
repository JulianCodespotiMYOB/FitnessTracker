using System.Diagnostics.CodeAnalysis;

namespace FitnessTracker.Models.Common;

public class Either<TL, TR>
{
    private TL? _left;
    private TR? _right;

    [MemberNotNullWhen(true, nameof(_left))]
    public bool IsLeft => _left != null;

    [MemberNotNullWhen(true, nameof(_right))]
    public bool IsRight => _right != null;

    public static Either<TL, TR> Left(TL value)
    {
        return new Either<TL, TR> { _left = value };
    }

    public static Either<TL, TR> Right(TR value)
    {
        return new Either<TL, TR> { _right = value };
    }

    public T Match<T>(Func<TL, T> left, Func<TR, T> right)
    {
        return IsLeft ? left(_left!) : right(_right!);
    }

    public Either<T, TT> Map<T, TT>(Func<TL, T> left, Func<TR, TT> right)
    {
        if (IsLeft)
        {
            return Either<T, TT>.Left(left(_left!));
        }

        return Either<T, TT>.Right(right(_right!));
    }

    public Either<T, TR> MapLeft<T>(Func<TL, T> left)
    {
        return IsLeft ? Either<T, TR>.Left(left(_left!)) : Either<T, TR>.Right(_right!);
    }

    public Either<TL, T> MapRight<T>(Func<TR, T> right)
    {
        return IsLeft ? Either<TL, T>.Left(_left!) : Either<TL, T>.Right(right(_right!));
    }
}

public static class EitherExtensions
{
    public static Either<TL, TR> ToEither<TL, TR>(this TL? value)
    {
        return value is null ? Either<TL, TR>.Right(default!) : Either<TL, TR>.Left(value);
    }

    public static Either<TL, TR> ToEither<TL, TR>(this TR? value)
    {
        return value is null ? Either<TL, TR>.Left(default!) : Either<TL, TR>.Right(value);
    }
}