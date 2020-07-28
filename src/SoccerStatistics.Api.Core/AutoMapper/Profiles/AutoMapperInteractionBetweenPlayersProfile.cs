using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperInteractionBetweenPlayersProfile : Profile
    {
        public AutoMapperInteractionBetweenPlayersProfile()
        {
            CreateMap<InteractionBetweenPlayers, InteractionBetweenPlayersDTO>();
        }
    }
}
