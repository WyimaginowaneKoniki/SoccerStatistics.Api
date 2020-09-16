using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database;
using System.Collections.Generic;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RepositoryTestBase
    {
        protected readonly IFakeData _fakeData;

        protected RepositoryTestBase()
        {
            _fakeData = new FakeData();
        }

        protected SoccerStatisticsDbContext GetInMemory<T>(string dbName, IEnumerable<T> data)
        {
            var options = new DbContextOptionsBuilder<SoccerStatisticsDbContext>()
                                .UseInMemoryDatabase(databaseName: dbName)
                                .Options;

            var context = new SoccerStatisticsDbContext(options);
                        
            foreach (T item in data)
                context.Add(item);

            context.SaveChanges();

            return context;
        }
    }
}
