﻿using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Buddy.Anatomy.Parts;

public class BuddyForearm : IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup => MuscleGroup.Forearm;
    public int Level { get; set; }
}