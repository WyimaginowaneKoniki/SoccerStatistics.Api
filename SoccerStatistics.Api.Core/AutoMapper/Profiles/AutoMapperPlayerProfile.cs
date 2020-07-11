using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    class AutoMapperPlayerProfile : Profile
    {
        public AutoMapperPlayerProfile()
        {
            CreateMap<Player, PlayerDTO>()
                .ForMember(dto => dto.TeamId, e => e.MapFrom(p => p.Team.Id))
                .ForMember(dto => dto.Age, 
                           e => e.MapFrom(p => (DateTime.UtcNow.Year - p.Birthday.Year)));
        }
    }
}
