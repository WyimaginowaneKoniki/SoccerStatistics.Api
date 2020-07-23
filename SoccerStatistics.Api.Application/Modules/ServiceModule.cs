using Autofac;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Linq;
using System.Reflection;

namespace SoccerStatistics.Api.Application.Modules
{
    public class ServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(LeagueService)
             .GetTypeInfo()
             .Assembly;

            builder.RegisterAssemblyTypes(assembly)
                    .Where(x => x.IsAssignableTo<IService>())
                    .AsImplementedInterfaces()
                    .InstancePerLifetimeScope();


    
        }
    }
}
