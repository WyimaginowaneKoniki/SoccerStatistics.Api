using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperTeamProfile : Profile
    {
        public AutoMapperTeamProfile()
        {
            CreateMap<Team, TeamDTO>()
                .ForMember(dto => dto.LeagueId, e => e.MapFrom(p => p.League.Id))
                .ForMember(dto => dto.StadiumId, e => e.MapFrom(p => p.Stadium.Id));

            CreateMap<Team, TeamBasicDTO>();

        }
    }
}
