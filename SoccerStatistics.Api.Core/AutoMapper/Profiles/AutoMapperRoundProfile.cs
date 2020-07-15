using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperRoundProfile : Profile
    {
        public AutoMapperRoundProfile()
        {
            CreateMap<Round, RoundDTO>()
              .ForMember(dto => dto.LeagueId, e => e.MapFrom(p => p.League.Id));
        }
    }
}
