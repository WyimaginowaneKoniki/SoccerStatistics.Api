using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperInteractionsBetweenPlayers : Profile
    {
        public AutoMapperInteractionsBetweenPlayers()
        {
            CreateMap<IEnumerable<InteractionBetweenPlayers>, IEnumerable<InteractionBetweenPlayersDTO>>();
        }
    }
}
