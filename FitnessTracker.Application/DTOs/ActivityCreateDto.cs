using FitnessTracker.Application.Helpers;
using FitnessTracker.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Application.DTOs
{
    public class ActivityCreateDto
    {
        [Required(ErrorMessage = "Title is required")]
        [StringLength(100, ErrorMessage = "Title can't be longer than 100 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Description is required")]
        [StringLength(500, ErrorMessage = "Description can't be longer than 500 characters")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Date and time are required")]
        [CustomValidation(typeof(ValidationHelper), nameof(ValidationHelper.ValidateDateNotInFuture))]
        public DateTime DateTime { get; set; }

        [Required(ErrorMessage = "Activity type is required")]
        [EnumDataType(typeof(ActivityType), ErrorMessage = "Invalid activity type")]
        public ActivityType ActivityType { get; set; }

        [Required(ErrorMessage = "Duration is required")]
        [CustomValidation(typeof(ValidationHelper), nameof(ValidationHelper.ValidatePositiveTimeSpan))]
        public TimeSpan Duration { get; set; }
    }
}
