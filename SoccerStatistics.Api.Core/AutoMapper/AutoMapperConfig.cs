using AutoMapper;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Core.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
            => new MapperConfiguration(cfg =>
           {
               cfg.AddProfile<AutoMapperPlayerProfile>();
           }).CreateMapper();
    }
}
