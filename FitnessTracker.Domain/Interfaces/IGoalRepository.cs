using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Domain.Interfaces
{
    /// <summary>
    /// Interface for accessing and managing goal entities in the repository.
    /// </summary>
    public interface IGoalRepository
    {
        /// <summary>
        /// Adds a new goal to the repository.
        /// </summary>
        /// <param name="goal">The goal entity to be added.</param>
        /// <returns>>A task representing the asynchronous operation, with a result of the goal entity</returns>
        Task<Goal> AddAsync(Goal goal);

        /// <summary>
        /// Updates an existing goal in the repository.
        /// </summary>
        /// <param name="goal">The goal entity with updated values.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        Task UpdateAsync(Goal goal);

        /// <summary>
        /// Retrieves goal by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>A task representing the asynchronous operation, with a result of the goal entity</returns>
        Task<Goal> GetByIdAsync(int id);

        /// <summary>
        /// Retrieves a queryable collection of <see cref="Goal"/> entities from the data context.
        /// </summary>
        /// <returns>
        /// An <see cref="IQueryable{Goal}"/> that represents the queryable set of goals. 
        /// This allows for further query composition, filtering, and sorting by the caller.
        /// </returns>
        IQueryable<Goal> GetGoalsQuery();
    }
}
