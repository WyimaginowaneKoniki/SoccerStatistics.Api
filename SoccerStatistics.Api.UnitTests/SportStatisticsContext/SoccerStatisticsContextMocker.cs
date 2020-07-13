using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;

namespace SoccerStatistics.Api.UnitTests.SportStatisticsContext
{
    public static class SoccerStatisticsContextMocker
    {
        public static IPlayerRepository GetInMemoryPlayerRepository(string dbName)
        {
            var options = new DbContextOptionsBuilder<SoccerStatisticsDbContext>()
                                .UseInMemoryDatabase(databaseName: dbName)
                                .Options;

            var soccerStatisticsDbContext = new SoccerStatisticsDbContext(options);
            soccerStatisticsDbContext.FillDatabaseWithPlayers();

            return new PlayerRepository(soccerStatisticsDbContext);
        }
        public static ITeamRepository GetInMemoryTeamRepository(string dbName)
        {
            var options = new DbContextOptionsBuilder<SoccerStatisticsDbContext>()
                                .UseInMemoryDatabase(databaseName: dbName)
                                .Options;

            var soccerStatisticsDbContext = new SoccerStatisticsDbContext(options);
            soccerStatisticsDbContext.FillDatabaseWithTeams();

            return new TeamRepository(soccerStatisticsDbContext);
        }
    }
}
