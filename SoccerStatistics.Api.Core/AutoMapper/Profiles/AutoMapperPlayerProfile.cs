using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperPlayerProfile : Profile
    {
        public AutoMapperPlayerProfile()
        {
            CreateMap<Player, PlayerDTO>()
                .ForMember(dto => dto.TeamId, e => e.MapFrom(p => p.Team.Id))
                .ForMember(dto => dto.Age, 
                           e => e.MapFrom(p => (DateTime.UtcNow.Year - p.Birthday.Year)));

            CreateMap<IEnumerable<Player>, IEnumerable<PlayerBasicDTO>>()
                .ForMember(dto => dto.Select(x => x.Age),
                           e => e.MapFrom(p => p.Select(x => DateTime.UtcNow.Year - x.Birthday.Year)));
        }
    }
}
