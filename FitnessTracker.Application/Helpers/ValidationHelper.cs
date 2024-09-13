using System.ComponentModel.DataAnnotations;

namespace FitnessTracker.Application.Helpers
{
    public static class ValidationHelper
    {
        public static ValidationResult ValidateDateNotInFuture(DateTime dateTime, ValidationContext context)
        {
            if (dateTime > DateTime.Now)
            {
                return new ValidationResult("Date and time cannot be in the future");
            }

            return ValidationResult.Success;
        }

        public static ValidationResult ValidatePositiveTimeSpan(TimeSpan duration, ValidationContext context)
        {
            if (duration <= TimeSpan.Zero)
            {
                return new ValidationResult("Duration must be a positive value");
            }

            return ValidationResult.Success;
        }
    }
}
