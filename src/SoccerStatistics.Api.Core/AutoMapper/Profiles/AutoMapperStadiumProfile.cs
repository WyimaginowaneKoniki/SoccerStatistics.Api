using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using System;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperStadiumProfile : Profile
    {
        public AutoMapperStadiumProfile()
        {
            CreateMap<Stadium, StadiumDTO>()
                  .ForMember(dto => dto.Cost, e => e.MapFrom(p => String.Format("{0:0.00} $", p.Cost)));

            CreateMap<Stadium, StadiumBasicDTO>();
        }
    }
}
