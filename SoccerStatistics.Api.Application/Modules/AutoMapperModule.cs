using Autofac;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SoccerStatistics.Api.Application.Modules
{
    public class AutoMapperModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblyNames = Assembly.GetExecutingAssembly().GetReferencedAssemblies();
            var assembliesTypes = assemblyNames
                .Where(a => a.Name.Equals("SoccerStatistics.Api.Core", StringComparison.OrdinalIgnoreCase))
                .SelectMany(an => Assembly.Load(an).GetTypes())
                .Where(p => typeof(Profile).IsAssignableFrom(p) && p.IsPublic && !p.IsAbstract)
                .Distinct();

            var autoMapperProfiles = assembliesTypes
                .Select(p => (Profile)Activator.CreateInstance(p)).ToList();

            builder.Register(ctx => new MapperConfiguration(cfg =>
            {
                foreach (var profile in autoMapperProfiles)
                {
                    cfg.AddProfile(profile);
                }
            }));

            builder.Register(ctx => ctx.Resolve<MapperConfiguration>().CreateMapper()).As<IMapper>().InstancePerLifetimeScope();
        }
    }
}
