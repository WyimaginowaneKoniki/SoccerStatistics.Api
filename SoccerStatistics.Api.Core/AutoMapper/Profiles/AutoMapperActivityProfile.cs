using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperActivityProfile : Profile
    {
        public AutoMapperActivityProfile()
        {
            CreateMap<Activity, ActivityDTO>();
        }
    }
}
