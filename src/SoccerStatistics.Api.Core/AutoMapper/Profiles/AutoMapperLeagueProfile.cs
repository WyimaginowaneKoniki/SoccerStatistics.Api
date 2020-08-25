using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperLeagueProfile : Profile
    {
        public AutoMapperLeagueProfile()
        {
            CreateMap<League, LeagueDTO>();
            CreateMap<League, LeagueBasicDTO>();
        }
    }
}
