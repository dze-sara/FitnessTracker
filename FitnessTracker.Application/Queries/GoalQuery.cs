using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Queries
{
    public class GoalQuery
    {
        public DateTime? Date { get; set; }
        public GoalType? Type { get; set; }
        public int? NumberOfActivities { get; set; }
        public TimeSpan? TotalDuration { get; set; }
    }
}
