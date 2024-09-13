namespace FitnessTracker.Common.Exceptions
{
    public class ActivityNotFoundException : Exception
    {
        public ActivityNotFoundException(int activityId) : base($"No activity found with id {activityId}.")
        {
        }
    }
}
