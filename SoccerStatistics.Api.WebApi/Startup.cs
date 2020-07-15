using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SoccerStatistics.Api.Application.Handlers;
using SoccerStatistics.Api.Core.AutoMapper;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;

namespace SoccerStatistics.Api.WebApi
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<SoccerStatisticsDbContext>();
            services.AddControllers()
                    .AddJsonOptions(x =>
                    {
                        // change json response formatting
                        x.JsonSerializerOptions.WriteIndented = true;
                    });

            services.AddMediatR(typeof(GetPlayerByIdHandler));
            services.AddMediatR(typeof(GetTeamByIdHandler));
            services.AddMediatR(typeof(GetLeagueByIdHandler));
            services.AddMediatR(typeof(GetAllLeaguesHandler));
            services.AddMediatR(typeof(GetRoundByIdHandler));

            services.AddSingleton(AutoMapperConfig.Initialize());

            services.AddScoped<ILeagueRepository, LeagueRepository>();
            services.AddScoped<ILeagueService, LeagueService>();


            services.AddScoped<IPlayerRepository, PlayerRepository>();
            services.AddScoped<IPlayerService, PlayerService>();

            services.AddScoped<ITeamRepository, TeamRepository>();
            services.AddScoped<ITeamService, TeamService>();

            services.AddScoped<IMatchRepository, MatchRepository>();
            services.AddScoped<IMatchService, MatchService>();

            services.AddScoped<IRoundRepository, RoundRepository>();
            services.AddScoped<IRoundService, RoundService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

    }
}
