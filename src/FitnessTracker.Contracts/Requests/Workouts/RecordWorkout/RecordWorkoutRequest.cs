using FitnessTracker.Models.Fitness.Datas;

namespace FitnessTracker.Contracts.Requests.Workouts.RecordWorkout;

public class RecordWorkoutRequest
{
    public int Id { get; set; }
    public string Name { get; set; } = $"{DateTime.Now:dd-MM-yyyy} Workout";
    public List<ActivityDto> Activities { get; set; } = new();
    public bool Completed { get; set; } = false;
    public bool Past { get; set; } = false;
    public DateTimeOffset Time { get; set; }
}

public record ActivityDto(int ExerciseId, Data Data);