using System.Text.Json.Serialization;
using FitnessTracker.Models.Buddy.Enums;
using FitnessTracker.Models.Users.Enum;

namespace FitnessTracker.Models.Users;

[JsonDerivedType(typeof(Experience), typeDiscriminator: nameof(RewardTypes.Experience))]
[JsonDerivedType(typeof(Title), typeDiscriminator: nameof(RewardTypes.Title))]
[JsonDerivedType(typeof(Badge), typeDiscriminator: nameof(RewardTypes.Badge))]
public abstract class Reward
{
    public int Id { get; set; }
    public virtual RewardTypes RewardType { get; }
}

public class Title : Reward
{
    public string Name { get; set; }
    public override RewardTypes RewardType => RewardTypes.Title;
}

public class Badge : Reward
{
    public string Name { get; set; }
    public Image Image { get; set; }
    public override RewardTypes RewardType => RewardTypes.Badge;
}

public class Experience : Reward
{
    public int Amount { get; set; }
    public StrengthLevelTypes StrengthLevel { get; set; }
    public override RewardTypes RewardType => RewardTypes.Experience;
}
