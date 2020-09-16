using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System.Linq;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperLeagueProfile : Profile
    {
        public AutoMapperLeagueProfile()
        {
            CreateMap<League, LeagueDTO>()
                .ForMember(dto => dto.Teams, e => e.MapFrom(l => l.Teams.Select(x => x.Team)));
            CreateMap<League, LeagueBasicDTO>();
        }
    }
}
