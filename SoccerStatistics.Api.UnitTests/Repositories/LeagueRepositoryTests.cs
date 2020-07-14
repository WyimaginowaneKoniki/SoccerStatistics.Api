using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class LeagueRepositoryTests
    {
        [Fact]
        public async Task ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetLeagueByIdReturnLeague");

            var expectedleague = new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = "Lionel Messi",
                Winner = "FC Barcelona",
            };

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testLeague);
            Assert.Equal(expectedleague.Id, testLeague.Id);
            Assert.Equal(expectedleague.Name, testLeague.Name);
            Assert.Equal(expectedleague.Country, testLeague.Country);
            Assert.Equal(expectedleague.Season, testLeague.Season);
            Assert.Equal(expectedleague.MVP, testLeague.MVP);
            Assert.Equal(expectedleague.Winner, testLeague.Winner);

        }

        [Fact]
        public async Task ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetLeagueByIdReturnNull");

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await repository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testLeague);
        }
    }
}
