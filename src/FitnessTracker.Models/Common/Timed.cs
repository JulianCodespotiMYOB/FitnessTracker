using System.Diagnostics;

namespace FitnessTracker.Application.Features.Exercises;

public class Timed<T>
{
    public T Result { get; set; }
    public double Time { get; set; }

    public Timed(T result, double time)
    {
        Result = result;
        Time = time;
    }

    public (T result, double time) Deconstruct()
    {
        return (Result, Time);
    }

    public static Timed<T> Record(Func<T> func)
    {
        Stopwatch stopwatch = new();
        stopwatch.Start();
        T result = func();
        stopwatch.Stop();
        return new(result, stopwatch.ElapsedMilliseconds);
    }
}
