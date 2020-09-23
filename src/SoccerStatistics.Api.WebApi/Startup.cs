using Autofac;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using SoccerStatistics.Api.Application.Modules;
using SoccerStatistics.Api.Database;
using System;
using System.IO;
using System.Reflection;

namespace SoccerStatistics.Api.WebApi
{
    public class Startup
    {
        private IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SoccerStatisticsDbContext>(options
                => options.UseSqlServer(Configuration["Database:ConnectionString"]));

            services.AddControllers()
                    .AddJsonOptions(x =>
                    {
                        // change json response formatting
                        x.JsonSerializerOptions.WriteIndented = true;
                    });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SoccerStatisticsApi",
                    Description = "API of Football World",
                });

        });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceModule>();
            builder.RegisterModule<RepositoryModule>();
            builder.RegisterModule<MediatoRModule>();
            builder.RegisterModule<AutoMapperModule>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, SoccerStatisticsDbContext dbContext,
            ILogger<Startup> logger, IDataInitializer dataInitializer)
        {
            dbContext.Database.EnsureCreated();
            logger.LogInformation("The database was created");

            if (Configuration.GetValue<bool>("Database:SeedData"))
            {
                dataInitializer.Seed();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API V1");
                c.RoutePrefix = "api/swagger";
            });
        }
    }
}
