using FitnessTracker.Domain.Enums;

namespace FitnessTracker.Application.Queries
{
    public class ActivityQuery
    {
        public string? Title { get; set; }
        public ActivityType? Type { get; set; }
        public DateTime? Date { get; set; }

        public bool IsValid()
        {
            var valid = false;

            if (!string.IsNullOrEmpty(Title)) 
            {
                valid = true;
            }

            if (Type.HasValue) 
            {
                valid = true;
            }

            if (Date.HasValue) 
            {
                valid = true;
            } 
            
            return valid;
        }
    }
}
