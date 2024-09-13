using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Domain.Interfaces
{
    /// <summary>
    /// Defines repository methods for managing activities.
    /// </summary>
    public interface IActivityRepository
    {
        /// <summary>
        /// Adds a new activity to the database.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task<Activity> AddAsync(Activity activity);

        /// <summary>
        /// Fetches a specific activity by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<Activity> GetByIdAsync(int id);

        /// <summary>
        /// Updates an existing activity.
        /// </summary>
        /// <param name="activity"></param>
        /// <returns></returns>
        Task UpdateAsync(Activity activity);

        /// <summary>
        /// Deletes an activity by its ID.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task DeleteAsync(int id);

        /// <summary>
        /// Retrieves a queryable collection of <see cref="Activity"/> entities from the data context.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{Activity}"/> that represents the queryable set of activities. 
        /// This allows for further query composition, filtering, and sorting by the caller.
        /// </returns>
        IQueryable<Activity> GetActivitiesQuery();
    }
}
