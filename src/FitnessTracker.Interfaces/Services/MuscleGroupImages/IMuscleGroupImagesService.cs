using FitnessTracker.Models.Fitness.Enums;
using Image = FitnessTracker.Models.Users.Image;

namespace FitnessTracker.Interfaces.Services;

public interface IMuscleGroupImagesService
{
    public Image GetImageForMuscleGroups(MuscleGroup primaryMuscleGroup, List<MuscleGroup>? otherMuscleGroups);
}