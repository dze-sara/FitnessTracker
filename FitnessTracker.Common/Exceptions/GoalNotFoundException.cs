namespace FitnessTracker.Common.Exceptions
{
    public class GoalNotFoundException : Exception
    {
        public GoalNotFoundException() : base("No goal found.")
        {
        }

        public GoalNotFoundException(int goalId) : base($"No goal found with id {goalId}.")
        {
        }
    }
}
