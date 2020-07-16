﻿using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperMatchProfile : Profile
    {
        public AutoMapperMatchProfile()
        {
            CreateMap<Match, MatchDTO>();
        }
    }
}