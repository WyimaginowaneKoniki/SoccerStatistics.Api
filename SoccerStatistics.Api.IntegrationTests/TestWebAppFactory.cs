using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.WebApi;
using System.Diagnostics;
using System.Linq;

namespace SoccerStatistics.Api.IntegrationTests
{
    public class TestWebAppFactory : WebApplicationFactory<Startup>
    {
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
           {
               // remove current context
               var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == typeof(DbContextOptions<SoccerStatisticsDbContext>)
                );

               if (descriptor != null)
               {
                   services.Remove(descriptor);
               }

               var serviceProvider = new ServiceCollection().AddEntityFrameworkInMemoryDatabase()
                                                            .BuildServiceProvider();

               services.AddDbContext<SoccerStatisticsDbContext>(options =>
               {
                   options.UseInMemoryDatabase("InMemory");
                   options.UseInternalServiceProvider(serviceProvider);
               });

               using var context = services.BuildServiceProvider()
                                           .CreateScope().ServiceProvider
                                           .GetRequiredService<SoccerStatisticsDbContext>();
               try
               {
                   context.Database.EnsureCreated();
                   context.FillDatabase();
               }
               catch (System.Exception e)
               {
                   //TODO: add logging here !!!
                   Debug.WriteLine(e);
                   throw;
               }
           });
        }
    }
}
