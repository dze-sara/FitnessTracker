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
    /// <summary>
    /// Service for managing goals
    /// </summary>
    public class GoalService : IGoalService
    {
        private readonly IGoalRepository _goalRepository;
        private readonly IMapper _mapper;

        public GoalService(IGoalRepository goalRepository, IMapper mapper)
        {
            _goalRepository = goalRepository ?? throw new ArgumentNullException(nameof(goalRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        /// <inheritdoc />
        public async Task<GoalDto> AddGoalAsync(GoalCreateDto goalCreateDto)
        {
            if (goalCreateDto == null)
            {
                throw new ArgumentException(nameof(goalCreateDto));
            }

            var goal = _mapper.Map<Goal>(goalCreateDto);
            var addedGoal = await _goalRepository.AddAsync(goal);
            return _mapper.Map<GoalDto>(addedGoal);
        }

        /// <inheritdoc />
        public async Task<GoalDto> GetGoalByIdAsync(int id)
        {
            var goal = await _goalRepository.GetByIdAsync(id);

            if (goal == null) throw new GoalNotFoundException();

            return _mapper.Map<GoalDto>(goal); ;
        }

        /// <inheritdoc />
        public async Task<IEnumerable<GoalDto>> GetGoalsAsync(GoalQuery query)
        {
            var goalsQuery = _goalRepository.GetGoalsQuery();

            if (query.TotalDuration.HasValue)
            {
                goalsQuery = goalsQuery.Where(x => x.TotalDuration == query.TotalDuration.Value);
            }

            if (query.Date.HasValue)
            {
                goalsQuery = goalsQuery.Where(x => x.Date == query.Date.Value.Date);
            }

            if (query.Type.HasValue)
            {
                goalsQuery = goalsQuery.Where(x => x.Type == query.Type.Value);
            }

            if (query.NumberOfActivities.HasValue)
            {
                goalsQuery = goalsQuery.Where(x => x.NumberOfActivities == query.NumberOfActivities.Value);
            }

            var goals = await goalsQuery.ToListAsync();
            return _mapper.Map<IEnumerable<GoalDto>>(goals);
        }

        /// <inheritdoc />
        public async Task UpdateGoalAsync(GoalUpdateDto goalUpdateDto)
        {
            if (goalUpdateDto == null)
            {
                throw new ArgumentException(nameof(goalUpdateDto));
            }

            var goal = _mapper.Map<Goal>(goalUpdateDto);
            await _goalRepository.UpdateAsync(goal);
        }
    }
}
