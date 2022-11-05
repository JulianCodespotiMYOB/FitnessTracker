﻿// <auto-generated />
using System;
using System.Collections.Generic;
using FitnessTracker.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace FitnessTracker.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-preview.7.22376.2")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("FitnessTracker.Models.Buddy.WorkoutBuddy", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("WorkoutBuddy");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Datas.Data", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<decimal?>("Distance")
                        .HasColumnType("numeric");

                    b.Property<decimal?>("Duration")
                        .HasColumnType("numeric");

                    b.Property<int?>("Reps")
                        .HasColumnType("integer");

                    b.Property<int?>("Sets")
                        .HasColumnType("integer");

                    b.Property<decimal>("TargetDistance")
                        .HasColumnType("numeric");

                    b.Property<int>("TargetReps")
                        .HasColumnType("integer");

                    b.Property<int>("TargetSets")
                        .HasColumnType("integer");

                    b.Property<decimal>("TargetWeight")
                        .HasColumnType("numeric");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.Property<decimal?>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.ToTable("Data");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Exercises.Exercise", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int?>("DetailedMuscleGroup")
                        .HasColumnType("integer");

                    b.Property<int>("Equipment")
                        .HasColumnType("integer");

                    b.Property<int>("MainMuscleGroup")
                        .HasColumnType("integer");

                    b.Property<int>("Mechanics")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int[]>("OtherMuscleGroups")
                        .HasColumnType("integer[]");

                    b.Property<int>("Type")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("Exercises");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Workouts.Activity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("DataId")
                        .HasColumnType("integer");

                    b.Property<int>("ExerciseId")
                        .HasColumnType("integer");

                    b.Property<int?>("WorkoutId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("DataId");

                    b.HasIndex("ExerciseId");

                    b.HasIndex("WorkoutId");

                    b.ToTable("Activity");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Workouts.Workout", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<bool>("Completed")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<bool>("Past")
                        .HasColumnType("boolean");

                    b.Property<DateTimeOffset>("Time")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Workouts");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<string>("Bytes")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("FileExtension")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Images");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Reward", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("RewardType")
                        .HasColumnType("integer");

                    b.Property<int?>("UserId")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Rewards");

                    b.HasDiscriminator<int>("RewardType");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<int>("Age")
                        .HasColumnType("integer");

                    b.Property<int?>("AvatarId")
                        .HasColumnType("integer");

                    b.Property<List<int>>("ClaimedAchievements")
                        .IsRequired()
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer[]")
                        .HasDefaultValue(new List<int>());

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<decimal>("Height")
                        .HasColumnType("numeric");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Username")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("WeeklyWorkoutAmountGoal")
                        .HasColumnType("integer");

                    b.Property<decimal>("Weight")
                        .HasColumnType("numeric");

                    b.HasKey("Id");

                    b.HasIndex("AvatarId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.UserSettings", b =>
                {
                    b.Property<int>("Id")
                        .HasColumnType("integer");

                    b.Property<bool>("DarkMode")
                        .HasColumnType("boolean");

                    b.Property<int>("MeasurementUnit")
                        .HasColumnType("integer");

                    b.Property<int>("WeightUnit")
                        .HasColumnType("integer");

                    b.HasKey("Id");

                    b.ToTable("UserSettings");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Badge", b =>
                {
                    b.HasBaseType("FitnessTracker.Models.Users.Reward");

                    b.Property<int>("ImageId")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasIndex("ImageId");

                    b.HasDiscriminator().HasValue(2);
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Experience", b =>
                {
                    b.HasBaseType("FitnessTracker.Models.Users.Reward");

                    b.Property<int>("Amount")
                        .HasColumnType("integer");

                    b.Property<int>("StrengthLevel")
                        .HasColumnType("integer");

                    b.HasDiscriminator().HasValue(0);
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Title", b =>
                {
                    b.HasBaseType("FitnessTracker.Models.Users.Reward");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasDiscriminator().HasValue(1);
                });

            modelBuilder.Entity("FitnessTracker.Models.Buddy.WorkoutBuddy", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.User", "User")
                        .WithOne("WorkoutBuddy")
                        .HasForeignKey("FitnessTracker.Models.Buddy.WorkoutBuddy", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Workouts.Activity", b =>
                {
                    b.HasOne("FitnessTracker.Models.Fitness.Datas.Data", "Data")
                        .WithMany()
                        .HasForeignKey("DataId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessTracker.Models.Fitness.Exercises.Exercise", "Exercise")
                        .WithMany()
                        .HasForeignKey("ExerciseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FitnessTracker.Models.Fitness.Workouts.Workout", null)
                        .WithMany("Activities")
                        .HasForeignKey("WorkoutId");

                    b.Navigation("Data");

                    b.Navigation("Exercise");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Workouts.Workout", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.User", null)
                        .WithMany("Workouts")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Reward", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.User", null)
                        .WithMany("Inventory")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.User", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.Image", "Avatar")
                        .WithMany()
                        .HasForeignKey("AvatarId");

                    b.Navigation("Avatar");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.UserSettings", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.User", "User")
                        .WithOne("UserSettings")
                        .HasForeignKey("FitnessTracker.Models.Users.UserSettings", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.Badge", b =>
                {
                    b.HasOne("FitnessTracker.Models.Users.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Image");
                });

            modelBuilder.Entity("FitnessTracker.Models.Fitness.Workouts.Workout", b =>
                {
                    b.Navigation("Activities");
                });

            modelBuilder.Entity("FitnessTracker.Models.Users.User", b =>
                {
                    b.Navigation("Inventory");

                    b.Navigation("UserSettings")
                        .IsRequired();

                    b.Navigation("WorkoutBuddy")
                        .IsRequired();

                    b.Navigation("Workouts");
                });
#pragma warning restore 612, 618
        }
    }
}
