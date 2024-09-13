using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Queries;

namespace FitnessTracker.Application.Interfaces
{
    /// <summary>
    /// Service interface for managing goals and their associated activities.
    /// </summary>
    public interface IGoalService
    {
        /// <summary>
        /// Adds a new goal including associated activities.
        /// </summary>
        /// <param name="goal">The goal to be added.</param>
        /// <returns>A task representing the asynchronous operation, with a result of the goal dto</returns>
        Task<GoalDto> AddGoalAsync(GoalCreateDto goal);

        /// <summary>
        /// Retrieves a goal by date.
        /// </summary>
        /// <param name="date"></param>
        /// <returns>A task representing the asynchronous operation, with the result of the goal if found.</returns>
        Task<IEnumerable<GoalDto>> GetGoalsAsync(GoalQuery query);

        /// <summary>
        /// Updates an existing goal including associated activities.
        /// </summary>
        /// <param name="goal">The updated goal information.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateGoalAsync(GoalUpdateDto goal);

        /// <summary>
        /// Retrieves a goal by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<GoalDto> GetGoalByIdAsync(int id);
    }
}
