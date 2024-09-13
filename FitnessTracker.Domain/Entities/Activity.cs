using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Domain.Entities
{
    public class Activity
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public ActivityType ActivityType { get; set; }
        public TimeSpan Duration { get; set; }
    }
}
