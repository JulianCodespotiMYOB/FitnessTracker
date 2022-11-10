using FitnessTracker.Interfaces.Services;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Users;
using SkiaSharp;

namespace FitnessTracker.Domain.MuscleGroupImages;

public class MuscleGroupImageGenerator : IMuscleGroupImagesService
{
    public Image GetImageForMuscleGroups(MuscleGroup primaryMuscleGroup, List<MuscleGroup>? otherMuscleGroups) 
    {
        SKImage primaryMuscleGroupImage = GetImageForMuscleGroup(primaryMuscleGroup);
        List<SKImage> otherMuscleGroupsImages =
            otherMuscleGroups.Contains(MuscleGroup.Cardio) || otherMuscleGroups.Contains(MuscleGroup.Unknown)
                ? new List<SKImage>()
                : otherMuscleGroups.Select(GetImageForMuscleGroup).ToList();
        SKImage mergedImage = MergeImages(primaryMuscleGroupImage, otherMuscleGroupsImages);
        return new Image()
        {
            Bytes = ImageToByteArray(mergedImage),
            Name = $"{primaryMuscleGroup.ToString()}_and_{otherMuscleGroupsImages.Count}_others.png",
            FileExtension = ".png"
        };
    }

    private SKImage GetImageForMuscleGroup(MuscleGroup muscleGroup)
    {
        string currentDirectory = Directory.GetCurrentDirectory();
        string directory = currentDirectory.Substring(0, currentDirectory.LastIndexOf('\\'));
        string muscleGroupAsString = muscleGroup.ToString() == "Unknown" || muscleGroup.ToString() == "Cardio" ? "Default" : muscleGroup.ToString();
        string path = $"{directory}\\FitnessTracker.Domain\\MuscleGroupImages\\Images\\{muscleGroupAsString}.png";
        return SKImage.FromBitmap(SKBitmap.Decode(path));
    }
    
    private SKImage MergeImages(SKImage primaryImage, List<SKImage> otherImages)
    {
        SKImage defaultImage = GetImageForMuscleGroup(MuscleGroup.Unknown);
        SKImageInfo info = new(defaultImage.Width, defaultImage.Height);
        using SKSurface surface = SKSurface.Create(info);
        SKCanvas canvas = surface.Canvas;
        canvas.DrawImage(defaultImage, 0, 0);
        int x = 0;
        int y = 0;
        otherImages.Add(primaryImage);
        foreach (SKImage image in otherImages)
        {
            canvas.DrawImage(image, x, y);
            x += image.Width;
            if (x >= primaryImage.Width)
            {
                x = 0;
                y += image.Height;
            }
        }
        return surface.Snapshot();
    }
    
    private string ImageToByteArray(SKImage imageIn)
    {
        using SKData data = imageIn.Encode(SKEncodedImageFormat.Png, 100);
        return Convert.ToBase64String(data.ToArray());
    }
}