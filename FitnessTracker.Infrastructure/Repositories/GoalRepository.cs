using FitnessTracker.Common.Exceptions;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces;
using FitnessTracker.Infrastructure.Data;
using FitnessTracker.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Infrastructure.Repositories
{
    public class GoalRepository : IGoalRepository
    {
        private readonly FitnessTrackerContext _dbContext;
        private readonly ILogger<GoalRepository> _logger;

        public GoalRepository(FitnessTrackerContext dbContext, ILogger<GoalRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<Goal> AddAsync(Goal goal)
        {
            if (goal == null)
            {
                _logger.LogWarning("Attempted to add a null goal.");
                throw new ArgumentException(nameof(goal));
            }

            try
            {
                _dbContext.Goals.Add(goal);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Goal with Id '{goal.Id}' added successfully.");
                return goal;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the goal with ID '{goal.Id}'.");
                throw new RepositoryException("An error occurred while adding the goal.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<Goal> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"Invalid Id {id} provided for retrieval.");
                throw new ArgumentException("Id must be greater than zero.", nameof(id));
            }

            return await _dbContext.Goals.FindAsync(id);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Goal goal)
        {
            if (goal == null)
            {
                _logger.LogWarning("Attempted to update a null goal.");
                throw new ArgumentException(nameof(goal));
            }

            try
            {
                var foundGoal = await _dbContext.Goals.FindAsync(goal.Id);

                if (foundGoal == null) throw new GoalNotFoundException(goal.Id);

                _dbContext.Goals.Update(goal);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Goal with Id '{goal.Id}' updated successfully.");
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"An error occurred while updating goal with ID '{goal.Id}'.");
                throw new RepositoryException("An error occurred while updating the goal.", ex);
            }
        }

        /// <inheritdoc />
        public IQueryable<Goal> GetGoalsQuery()
        {
            return _dbContext.Goals.AsQueryable();
        }
    }
}
