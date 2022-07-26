﻿using FitnessTracker.Models.Fitness.Enums;

namespace FitnessTracker.Models.Buddy.Anatomy;

public interface IBuddyAnatomy
{
    public int Id { get; set; }
    public MuscleGroup MuscleGroup { get; }
    public int Level { get; set; }
}