using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Infrastructure.Data
{
    public static class FitnessTrackerDbSeed
    {
        public static async Task SeedAsync(FitnessTrackerContext context)
        {
            if (!context.Activities.Any())
            {
                context.Activities.AddRange(
                    new Activity { Title = "Morning Run", Description = "5km run", DateTime = DateTime.Now.AddDays(-20), ActivityType = ActivityType.Run, Duration = TimeSpan.FromMinutes(30) },
                    new Activity { Title = "Evening Walk", Description = "Leisure walk", DateTime = DateTime.Now.AddDays(-19), ActivityType = ActivityType.Walk, Duration = TimeSpan.FromMinutes(45) },
                    new Activity { Title = "Mountain Hike", Description = "Hiking adventure", DateTime = DateTime.Now.AddDays(-18), ActivityType = ActivityType.Hike, Duration = TimeSpan.FromHours(3) },
                    new Activity { Title = "Swimming", Description = "Swimming laps", DateTime = DateTime.Now.AddDays(-17), ActivityType = ActivityType.Swim, Duration = TimeSpan.FromMinutes(60) },
                    new Activity { Title = "Morning HIIT", Description = "High-intensity interval training", DateTime = DateTime.Now.AddDays(-16), ActivityType = ActivityType.HIIT, Duration = TimeSpan.FromMinutes(20) },
                    new Activity { Title = "Bike Ride", Description = "Casual ride", DateTime = DateTime.Now.AddDays(-15), ActivityType = ActivityType.Ride, Duration = TimeSpan.FromMinutes(40) },
                    new Activity { Title = "Gym Workout", Description = "Weightlifting", DateTime = DateTime.Now.AddDays(-14), ActivityType = ActivityType.Workout, Duration = TimeSpan.FromMinutes(90) },
                    new Activity { Title = "HIIT Cardio", Description = "Cardio workout", DateTime = DateTime.Now.AddDays(-13), ActivityType = ActivityType.HIIT, Duration = TimeSpan.FromMinutes(30) },
                    new Activity { Title = "Afternoon Walk", Description = "Walk in the park", DateTime = DateTime.Now.AddDays(-12), ActivityType = ActivityType.Walk, Duration = TimeSpan.FromMinutes(30) },
                    new Activity { Title = "Swimming", Description = "Pool workout", DateTime = DateTime.Now.AddDays(-11), ActivityType = ActivityType.Swim, Duration = TimeSpan.FromMinutes(60) },
                    new Activity { Title = "Evening Yoga", Description = "Relaxing yoga session", DateTime = DateTime.Now.AddDays(-10), ActivityType = ActivityType.Other, Duration = TimeSpan.FromMinutes(45) },
                    new Activity { Title = "Running Intervals", Description = "Interval training run", DateTime = DateTime.Now.AddDays(-9), ActivityType = ActivityType.Run, Duration = TimeSpan.FromMinutes(25) },
                    new Activity { Title = "Cycling", Description = "Mountain biking", DateTime = DateTime.Now.AddDays(-8), ActivityType = ActivityType.Ride, Duration = TimeSpan.FromMinutes(50) },
                    new Activity { Title = "Strength Training", Description = "Full-body workout", DateTime = DateTime.Now.AddDays(-7), ActivityType = ActivityType.Workout, Duration = TimeSpan.FromMinutes(60) },
                    new Activity { Title = "Kayaking", Description = "Paddle sports", DateTime = DateTime.Now.AddDays(-6), ActivityType = ActivityType.Other, Duration = TimeSpan.FromHours(1) },
                    new Activity { Title = "Pilates", Description = "Core strengthening", DateTime = DateTime.Now.AddDays(-5), ActivityType = ActivityType.Other, Duration = TimeSpan.FromMinutes(50) },
                    new Activity { Title = "Jogging", Description = "Morning jog", DateTime = DateTime.Now.AddDays(-4), ActivityType = ActivityType.Run, Duration = TimeSpan.FromMinutes(35) },
                    new Activity { Title = "Stretching", Description = "Full-body stretch", DateTime = DateTime.Now.AddDays(-3), ActivityType = ActivityType.Other, Duration = TimeSpan.FromMinutes(30) },
                    new Activity { Title = "Hiking Trail", Description = "Trail hike", DateTime = DateTime.Now.AddDays(-2), ActivityType = ActivityType.Hike, Duration = TimeSpan.FromHours(2) },
                    new Activity { Title = "Rowing", Description = "Rowing machine workout", DateTime = DateTime.Now.AddDays(-1), ActivityType = ActivityType.Other, Duration = TimeSpan.FromMinutes(40) },
                    new Activity { Title = "Interval Training", Description = "High-intensity intervals", DateTime = DateTime.Now, ActivityType = ActivityType.HIIT, Duration = TimeSpan.FromMinutes(25) },
                    new Activity { Title = "Evening Jog", Description = "Relaxed evening jog", DateTime = DateTime.Now.AddDays(1), ActivityType = ActivityType.Run, Duration = TimeSpan.FromMinutes(30) },
                    new Activity { Title = "Early Morning Walk", Description = "Morning walk", DateTime = DateTime.Now.AddDays(2), ActivityType = ActivityType.Walk, Duration = TimeSpan.FromMinutes(45) }
                );
            }

            if (!context.Goals.Any())
            {
                context.Goals.AddRange(
                    new Goal { Date = DateTime.Now.AddDays(-5), NumberOfActivities = 5, TotalDuration = null, Type = GoalType.ActivityCount },
                    new Goal { Date = DateTime.Now.AddDays(-4), NumberOfActivities = null, TotalDuration = TimeSpan.FromHours(2), Type = GoalType.Duration },
                    new Goal { Date = DateTime.Now.AddDays(-3), NumberOfActivities = 3, TotalDuration = null, Type = GoalType.ActivityCount },
                    new Goal { Date = DateTime.Now.AddDays(-2), NumberOfActivities = null, TotalDuration = TimeSpan.FromMinutes(90), Type = GoalType.Duration },
                    new Goal { Date = DateTime.Now.AddDays(-1), NumberOfActivities = 4, TotalDuration = null, Type = GoalType.ActivityCount },
                    new Goal { Date = DateTime.Now, NumberOfActivities = null, TotalDuration = TimeSpan.FromMinutes(120), Type = GoalType.Duration },
                    new Goal { Date = DateTime.Now.AddDays(1), NumberOfActivities = 2, TotalDuration = null, Type = GoalType.ActivityCount },
                    new Goal { Date = DateTime.Now.AddDays(2), NumberOfActivities = null, TotalDuration = TimeSpan.FromHours(1.5), Type = GoalType.Duration },
                    new Goal { Date = DateTime.Now.AddDays(3), NumberOfActivities = 6, TotalDuration = null, Type = GoalType.ActivityCount },
                    new Goal { Date = DateTime.Now.AddDays(4), NumberOfActivities = null, TotalDuration = TimeSpan.FromMinutes(75), Type = GoalType.Duration }
                );
            }

            await context.SaveChangesAsync();
        }
    }
}
