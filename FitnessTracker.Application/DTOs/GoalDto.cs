using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.DTOs
{
    public class GoalDto
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public GoalType Type { get; set; }
        public int? NumberOfActivities { get; set; }
        public TimeSpan? TotalDuration { get; set; }
    }
}
