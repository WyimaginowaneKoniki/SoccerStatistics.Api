using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RepositoryTestBase
    {
        protected SoccerStatisticsDbContext _context;

        protected readonly IFakeData _fakeData;

        protected RepositoryTestBase()
        {
            _fakeData = new FakeData();
        }

        protected SoccerStatisticsDbContext GetInMemory(string dbName)
        {
            var options = new DbContextOptionsBuilder<SoccerStatisticsDbContext>()
                                .UseInMemoryDatabase(databaseName: dbName)
                                .Options;

            return new SoccerStatisticsDbContext(options);
        }
    }
}
