using AutoMapper;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Application.Interfaces;
using FitnessTracker.Application.Queries;
using FitnessTracker.Common.Exceptions;
using FitnessTracker.Domain.Entities;
using FitnessTracker.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace FitnessTracker.Application.Services
{
    public class ActivityService : IActivityService
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IMapper _mapper;

        public ActivityService(IActivityRepository activityRepository, IMapper mapper)
        {
            _activityRepository = activityRepository;
            _mapper = mapper;
        }

        /// <inheritdoc />
        public async Task<ActivityDto> CreateActivityAsync(ActivityCreateDto activityDto)
        {
            if (activityDto == null) throw new ArgumentException(nameof(activityDto));

            var activity = _mapper.Map<Activity>(activityDto);
            var addedActivity = await _activityRepository.AddAsync(activity);
            return _mapper.Map<ActivityDto>(addedActivity);
        }

        /// <inheritdoc />
        public async Task DeleteActivityAsync(int id)
        {
            await _activityRepository.DeleteAsync(id);
        }

        /// <inheritdoc />
        public async Task<IEnumerable<ActivityDto>> GetActivitiesAsync(ActivityQuery query)
        {
            var activitiesQuery = _activityRepository.GetActivitiesQuery();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                activitiesQuery = activitiesQuery.Where(a => a.Title.Contains(query.Title));
            }

            if (query.Type.HasValue)
            {
                activitiesQuery = activitiesQuery.Where(a => a.ActivityType == query.Type.Value);
            }

            if (query.Date.HasValue)
            {
                activitiesQuery = activitiesQuery.Where(a => a.DateTime.Date == query.Date.Value.Date);
            }

            var activities = await activitiesQuery.ToListAsync();
            return _mapper.Map<IEnumerable<ActivityDto>>(activities);
        }

        /// <inheritdoc />
        public async Task<ActivityDto> GetActivityByIdAsync(int id)
        {
            var activity = await _activityRepository.GetByIdAsync(id);

            if (activity == null) throw new ActivityNotFoundException(id);

            return _mapper.Map<ActivityDto>(activity);
        }

        /// <inheritdoc />
        public async Task UpdateActivityAsync(int id, ActivityUpdateDto activityDto)
        {
            if (activityDto == null) throw new ArgumentException(nameof(activityDto));

            var activity = await _activityRepository.GetByIdAsync(id);
            if (activity == null) throw new ActivityNotFoundException(id);


            var newActivity = _mapper.Map<Activity>(activityDto);
            await _activityRepository.UpdateAsync(newActivity);
        }
    }
}
