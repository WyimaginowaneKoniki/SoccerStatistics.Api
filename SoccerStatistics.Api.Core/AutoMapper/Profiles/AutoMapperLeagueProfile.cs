using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperLeagueProfile : Profile
    {
        public AutoMapperLeagueProfile()
        {
            CreateMap<League, LeagueDTO>();
        }
    }
}
