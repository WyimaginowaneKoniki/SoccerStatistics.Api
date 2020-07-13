using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperMatchProfile : Profile
    {
        public AutoMapperMatchProfile()
        {
            CreateMap<Match, MatchDTO>()
                .ForMember(dto => dto.StadiumId, e => e.MapFrom(match => match.Stadium.Id))
                .ForMember(dto => dto.RoundId, e => e.MapFrom(match => match.Round.Id))
                .ForMember(dto => dto.TeamInMatchStatsId, e => e.MapFrom(match => match.TeamInMatchStats.Id))
                .ForMember(dto => dto.MatchTeam1Id, e => e.MapFrom(match => match.MatchTeam1.Id))
                .ForMember(dto => dto.MatchTeam2Id, e => e.MapFrom(match => match.MatchTeam2.Id));
        }
    }
}