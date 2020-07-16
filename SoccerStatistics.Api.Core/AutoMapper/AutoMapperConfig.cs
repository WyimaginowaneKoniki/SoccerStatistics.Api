using AutoMapper;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;

namespace SoccerStatistics.Api.Core.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<AutoMapperPlayerProfile>();
               cfg.AddProfile<AutoMapperMatchProfile>();
               cfg.AddProfile<AutoMapperTeamProfile>();
               cfg.AddProfile<AutoMapperTeamInMatchStatsProfile>();
               cfg.AddProfile<AutoMapperStadiumProfile>();
               cfg.AddProfile<AutoMapperRoundProfile>();
               cfg.AddProfile<AutoMapperInteractionsBetweenPlayers>();
               cfg.AddProfile<AutoMapperLeagueProfile>();
           }).CreateMapper();

    }
}
