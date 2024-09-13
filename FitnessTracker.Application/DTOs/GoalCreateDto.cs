using FitnessTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Application.DTOs
{
    /// <summary>
    /// Data Transfer Object for creating a new goal.
    /// </summary>
    public class GoalCreateDto
    {
        /// <summary>
        /// The target number of activities for the goal. This is required if the goal type is ActivityCount.
        /// </summary>
        [Range(1, int.MaxValue, ErrorMessage = "Number of activities must be greater than zero.")]
        public int? NumberOfActivities { get; set; }

        /// <summary>
        /// The target total duration for the goal. This is required if the goal type is Duration.
        /// </summary>
        public TimeSpan? TotalDuration { get; set; }

        /// <summary>
        /// The date for which the goal is set.
        /// </summary>
        [Required(ErrorMessage = "Date is required")]
        public DateTime Date { get; set; }

        /// <summary>
        /// The type of the goal (ActivityCount or Duration).
        /// </summary>
        [Required(ErrorMessage = "Goal type is required")]
        [EnumDataType(typeof(GoalType), ErrorMessage = "Invalid goal type")]
        public GoalType Type { get; set; }

        /// <summary>
        /// Custom validation to ensure that exactly one of NumberOfActivities or TotalDuration is provided based on GoalType.
        /// </summary>
        public bool Validate()
        {
            if (TotalDuration.HasValue && TotalDuration.Value.TotalMilliseconds < 0)
            {
                return false;
            }

            if (Type == GoalType.ActivityCount && !NumberOfActivities.HasValue)
            {
                return false;
            }

            if (Type == GoalType.Duration && !TotalDuration.HasValue)
            {
                return false;
            }

            return true;
        }
    }
}
