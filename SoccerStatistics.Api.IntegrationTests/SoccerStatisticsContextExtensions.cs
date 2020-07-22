using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;

namespace SoccerStatistics.Api.IntegrationTests
{
    public static class SportStatisticsContextExtensions
    {
        private static readonly IEnumerable<League> leagues = new List<League>()
        {
            new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = "Lionel Messi",
                Winner = "FC Barcelona"
            },
            new League()
            {
                Id = 2,
                Name = "Serie A",
                Country = "Italia",
                Season = "2017/2018",
                MVP = "Mauro Icardi",
                Winner = "Juventus"
            },
            new League()
            {
                Id = 3,
                Name = "Lotto Ekstraklasa",
                Country = "Poland",
                Season = "2018/2019",
                MVP = "Igor Angulo",
                Winner = "Piast Gliwice"
            }
        };

        public async static void FillDatabase(this SoccerStatisticsDbContext context)
        {
            await context.AddRangeAsync(leagues);
            await context.SaveChangesAsync();
        }
    }
}
