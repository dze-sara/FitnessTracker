using AutoMapper;
using FitnessTracker.Application.DTOs;
using FitnessTracker.Domain.Entities;

namespace FitnessTracker.Application.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Activity, ActivityDto>();
            CreateMap<ActivityCreateDto, Activity>();
            CreateMap<ActivityUpdateDto, Activity>();

            CreateMap<Goal, GoalDto>();
            CreateMap<GoalCreateDto, Goal>();
            CreateMap<GoalUpdateDto, Goal>();
        }
    }
}
