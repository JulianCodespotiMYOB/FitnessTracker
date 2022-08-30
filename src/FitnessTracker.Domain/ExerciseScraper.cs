using FitnessTracker.Models.Common;
using FitnessTracker.Models.Fitness;
using FitnessTracker.Models.Fitness.Exercises;
using HtmlAgilityPack;

namespace FitnessTracker.Domain;

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
            listOfLinksToExercises.Add(doc.DocumentNode.SelectNodes("//a[@style='color:#0E709A;']"));
            HtmlNode? nextButtonLink = doc.DocumentNode.SelectSingleNode("//a[@rel=\"next\"]");

            if (nextButtonLink is null)
            {
                nextPageExists = false;
                break;
            }

            url = nextButtonLink.Attributes["href"].Value;
            string cleanUrl = string.Concat("https://www.jefit.com/exercises", url.AsSpan(1));
            doc = web.Load(cleanUrl);
        }

        List<Exercise> exercises = new();
        foreach (HtmlNodeCollection linksToExercise in listOfLinksToExercises)
        {
            foreach(var linkToExercise in linksToExercise)
            {
                try
                {
                    url = linkToExercise.Attributes["href"].Value;
                    string cleanUrl = "https://www.jefit.com/exercises/" + url;
                    doc = web.Load(cleanUrl);
                    IEnumerable<string> exerciseName = url.Split('/').Skip(1);
                    string excerciseNameClean = string.Join("/", exerciseName).Replace("-", " ");
                    int j = 3;
                    int k = 3;
                    for (int i = 0; i < 10; i++)
                    {
                        HtmlNode? mainMuscleGroupTest =
                            doc.DocumentNode.SelectSingleNode(
                                $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{i}]/div[2]/p[1]");

                        if (mainMuscleGroupTest is null)
                        {
                            continue;
                        }

                        j = i;
                        for (int p = 0; p < 10; p++)
                        {
                            HtmlNode typeTest =
                                doc.DocumentNode.SelectSingleNode(
                                    $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{p}]/div[2]/p[4]");

                            if (typeTest is null)
                            {
                                continue;
                            }

                            k = p;
                            break;
                        }
                    }

                    HtmlNode? mainMuscleGroup =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{j}]/div[2]/p[1]");
                    HtmlNode? otherMuscleGroups =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{j}]/div[2]/p[2]");
                    HtmlNode? detailedMuscleGroup =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{j}]/div[2]/p[3]");
                    HtmlNode type =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{k}]/div[2]/p[4]");
                    HtmlNode mechanics =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{k}]/div[2]/p[5]");
                    HtmlNode equipment =
                        doc.DocumentNode.SelectSingleNode(
                            $"//*[@id=\"page\"]/div/div[3]/div/div[1]/div[3]/div[{k}]/div[2]/p[6]");

                    Exercise exercise = new()
                    {
                        Name = excerciseNameClean,
                        MainMuscleGroup = MuscleGroupExtensions.FromName(mainMuscleGroup.InnerText),
                        OtherMuscleGroups = otherMuscleGroups is null
                            ? new List<MuscleGroup>()
                            : otherMuscleGroups.InnerText.Split(',').Select(MuscleGroupExtensions.FromName).ToList(),
                        DetailedMuscleGroup = MuscleGroupExtensions.FromName(detailedMuscleGroup?.InnerText),
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
            };
        }

        return Result<List<Exercise>>.Success(exercises);
    }
}