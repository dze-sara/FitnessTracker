using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Queries;

namespace FitnessTracker.Application.Interfaces
{
    /// <summary>
    /// Defines service operations for managing activities.
    /// </summary>
    public interface IActivityService
    {
        /// <summary>
        /// Retrieves list of activities by search query
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<IEnumerable<ActivityDto>> GetActivitiesAsync(ActivityQuery query);

        /// <summary>
        /// Retrieves a specific activity by identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ActivityDto> GetActivityByIdAsync(int id);

        /// <summary>
        /// Creates a new activity based on the provided data.
        /// </summary>
        /// <param name="newActivity"></param>
        /// <returns></returns>
        Task<ActivityDto> CreateActivityAsync(ActivityCreateDto newActivity);

        /// <summary>
        /// Updates an existing activity with the specified identifier using the provided data.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updatedActivity"></param>
        /// <returns></returns>
        Task UpdateActivityAsync(int id, ActivityUpdateDto updatedActivity);

        /// <summary>
        /// Deletes the activity with the specified identifier.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteActivityAsync(int id);
    }
}
