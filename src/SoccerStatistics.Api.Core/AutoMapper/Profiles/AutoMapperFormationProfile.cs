using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;

namespace SoccerStatistics.Api.Core.AutoMapper.Profiles
{
    public class AutoMapperFormationProfile : Profile
    {
        public AutoMapperFormationProfile()
        {
            CreateMap<Formation, FormationDTO>();
        }
    }
}
