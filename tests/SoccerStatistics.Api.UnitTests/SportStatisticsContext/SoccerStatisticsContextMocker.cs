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
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithPlayers();

            return new PlayerRepository(soccerStatisticsDbContext);
        }

        public static IMatchRepository GetInMemoryMatchRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithMatches();

            return new MatchRepository(soccerStatisticsDbContext);
        }

      
        public static ITeamRepository GetInMemoryTeamRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithTeams();

            return new TeamRepository(soccerStatisticsDbContext);
        }

        public static ILeagueRepository GetInMemoryLeagueRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithLeagues();

            return new LeagueRepository(soccerStatisticsDbContext);
        }
        public static IRoundRepository GetInMemoryRoundRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithRounds();

            return new RoundRepository(soccerStatisticsDbContext);
        }

        public static IStadiumRepository GetInMemoryStadiumRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithStadiums();

            return new StadiumRepository(soccerStatisticsDbContext);
        }
        public static IMatchRepository GetInMemoryHistoryOfMatchesRepository(string dbName)
        {
            var soccerStatisticsDbContext = InitSoccerStatisticsDbContext(dbName);
            soccerStatisticsDbContext.FillDatabaseWithMatchesAndRounds();

            return new MatchRepository(soccerStatisticsDbContext);
        }

        private static SoccerStatisticsDbContext InitSoccerStatisticsDbContext(string dbName)
        {
            var options = new DbContextOptionsBuilder<SoccerStatisticsDbContext>()
                                .UseInMemoryDatabase(databaseName: dbName)
                                .Options;
            return new SoccerStatisticsDbContext(options);
        }
    }
}
