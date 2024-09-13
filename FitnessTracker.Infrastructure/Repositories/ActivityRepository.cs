using FitnessTracker.Common.Exceptions;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces;
using FitnessTracker.Infrastructure.Data;
using FitnessTracker.Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace FitnessTracker.Infrastructure.Repositories
{
    public class ActivityRepository : IActivityRepository
    {
        private readonly FitnessTrackerContext _dbContext;
        private readonly ILogger<ActivityRepository> _logger;

        public ActivityRepository(FitnessTrackerContext dbContext, ILogger<ActivityRepository> logger)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <inheritdoc />
        public async Task<Activity> AddAsync(Activity activity)
        {
            if (activity == null) 
            {
                _logger.LogWarning("Attempted to add a null activity.");
                throw new ArgumentException(nameof(activity));
            } 

            try
            {
                await _dbContext.Activities.AddAsync(activity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Activity with title '{activity.Title}' added successfully.");

                return activity;
            }
            catch (DbUpdateException ex)
            {
                _logger.LogError(ex, $"An error occurred while adding the activity with title '{activity.Title}'.");
                throw new RepositoryException("An error occurred while adding the activity.", ex);
            }
        }

        /// <inheritdoc />
        public async Task DeleteAsync(int id)
        {
            try
            {
                var activity = await _dbContext.Activities.FindAsync(id);
                if (activity == null) throw new ActivityNotFoundException(id);
                
                _dbContext.Activities.Remove(activity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Activity with Id {id} deleted successfully.");
                
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"A concurrency error occurred while deleting activity with Id {id}.");
                throw new RepositoryException("A concurrency error occurred while deleting the activity.", ex);
            }
        }

        /// <inheritdoc />
        public async Task<Activity> GetByIdAsync(int id)
        {
            if (id <= 0)
            {
                _logger.LogWarning($"Invalid Id {id} provided for retrieval.");
                throw new ArgumentException("Id must be greater than zero.", nameof(id));
            }

            return await _dbContext.Activities.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <inheritdoc />
        public async Task UpdateAsync(Activity activity)
        {
            if (activity == null) 
            {
                _logger.LogWarning("Attempted to update a null activity.");
                throw new ArgumentException(nameof(activity));
            } 

            try
            {
                _dbContext.Activities.Update(activity);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Activity with Id {activity.Id} updated successfully.");
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogError(ex, $"A concurrency error occurred while updating activity with Id {activity.Id}.");
                throw new RepositoryException("A concurrency error occurred while updating the activity.", ex);
            }
        }

        /// <inheritdoc />
        public IQueryable<Activity> GetActivitiesQuery()
        {
            return _dbContext.Activities.AsQueryable();
        }
    }
}
