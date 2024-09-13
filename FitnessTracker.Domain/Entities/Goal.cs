using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Domain.Entities
{
    public class Goal
    {
        public int Id { get; set; }
        public int? NumberOfActivities { get; set; }
        public TimeSpan? TotalDuration { get; set; }
        public DateTime Date { get; set; }
        public GoalType Type { get; set; }
    }
}
