using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness.Enums;
using FitnessTracker.Models.Fitness.Exercises;
using FitnessTracker.Models.Fitness.Extensions;
using FitnessTracker.Models.Users;
using HtmlAgilityPack;

namespace FitnessTracker.Domain.Exercises;

public static class ExerciseScraper
{
    public static Result<List<Exercise>> ScrapeExercises()
    {
        string url = "https://www.jefit.com/exercises/bodypart.php?id=11&exercises=All";
        bool nextPageExists = true;
        List<HtmlNodeCollection>? listOfLinksToExercises = new();
        
        HtmlWeb web = new();
        HtmlDocument doc = web.Load(url);
        
        while (nextPageExists)
        {
            try 
            {
                listOfLinksToExercises.Add(doc.DocumentNode.SelectNodes("//a[@style='color:#0E709A;']"));
                HtmlNode? nextButtonLink = doc.DocumentNode.SelectSingleNode("//a[@rel=\"next\"]");
        
                if (nextButtonLink is null)
                {
                    break;
                }
        
                url = nextButtonLink.Attributes["href"].Value;
                string cleanUrl = string.Concat("https://www.jefit.com/exercises", url.AsSpan(1));
                doc = web.Load(cleanUrl);
            } 
            catch (Exception e)
            {
                break;
            }
        }
        
        List<Exercise> exercises = new();
        foreach (HtmlNodeCollection linksToExercise in listOfLinksToExercises)
        {
            foreach (HtmlNode? linkToExercise in linksToExercise)
            {
                try
                {
                    url = linkToExercise.Attributes["href"].Value;
                    string cleanUrl = "https://www.jefit.com/exercises/" + url;
                    doc = web.Load(cleanUrl);
                    IEnumerable<string> exerciseName = url.Split('/').Skip(1);
                    string exerciseNameClean = string.Join("/", exerciseName).Replace("-", " ");
                    int j = 3;
                    int k = 3;
        
                    //get node that has text <strong>Main Muscle Group :</strong>
                    HtmlNode? mainMuscleGroup = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()='Main Muscle Group :']").ParentNode.ChildNodes[1];
                    HtmlNode? otherMuscleGroups = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()='Other Muscle Groups : ']")?.ParentNode.ChildNodes[1];
                    HtmlNode? detailedMuscleGroup = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()='Detailed Muscle Group : ']")?.ParentNode.ChildNodes[1];
                    HtmlNode type = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()='Type :']").ParentNode.ChildNodes[1];
                    HtmlNode mechanics = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()=' Mechanics :']").ParentNode.ChildNodes[1];
                    HtmlNode equipment = doc.DocumentNode
                        .SelectSingleNode($"//strong[text()=' Equipment :']").ParentNode.ChildNodes[1];
        
                    Exercise exercise = new()
                    {
                        Name = exerciseNameClean,
                        MainMuscleGroup = MuscleGroupExtensions.FromName(mainMuscleGroup.InnerText),
                        OtherMuscleGroups = otherMuscleGroups.InnerText is null
                            ? new List<MuscleGroup>()
                            : otherMuscleGroups.InnerText.Split(',').Select(MuscleGroupExtensions.FromName).ToList(),
                        DetailedMuscleGroup = MuscleGroupExtensions.FromNameDetailed(detailedMuscleGroup?.InnerText),
                        MuscleGroupImage = new Image(),
                        Type = ExerciseTypeExtensions.FromName(type.InnerText),
                        Mechanics = MechanicsExtensions.FromName(mechanics.InnerText),
                        Equipment = EquipmentExtensions.FromName(equipment.InnerText)
                    };
        
                    exercises.Add(exercise);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        return Result<List<Exercise>>.Success(exercises);
    }
}