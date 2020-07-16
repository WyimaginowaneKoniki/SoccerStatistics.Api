using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperRoundProfile : Profile
    {
        public AutoMapperRoundProfile()
        {
            CreateMap<Round, RoundBasicDTO>();
            CreateMap<Round, RoundDTO>();
        }
    }
}
