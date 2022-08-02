namespace FitnessTracker.Contracts.Requests.Workout;

public class RecordWorkoutRequest
{
    public int UserId { get; set; }
    public Models.Excercises.Workout.Workout Workout { get; set; }
}