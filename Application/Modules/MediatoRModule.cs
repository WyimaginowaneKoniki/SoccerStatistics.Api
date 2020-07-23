using System;
using System.Linq;
using System.Reflection;
using Autofac;
using AutoMapper;
using MediatR;

namespace SoccerStatistics.Api.Application.Modules
{
    public class MediatoRModule : Autofac.Module
    {
       
        protected override void Load(ContainerBuilder builder)
        {
            var assembly = typeof(MediatoRModule)
                .GetTypeInfo()
                .Assembly;

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly)
                   .AsImplementedInterfaces();

            builder.RegisterAssemblyTypes(assembly)
                .AsClosedTypesOf(typeof(IRequestHandler<,>))
                .InstancePerLifetimeScope();

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }
    }
}