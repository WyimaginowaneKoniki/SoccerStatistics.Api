using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class LeagueRepositoryTests
    {

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetAllLeagues");
            IEnumerable<League> expectedleagues = new List<League>
            {

                new League()
                {
                  Id = 1,
                  Name = "Primera Division",
                  Country = "Spain",
                  Season = "2018/2019",
                  MVP = "Lionel Messi",
                  Winner = "FC Barcelona",
                  Rounds = null,
                  Teams = null
                  },
            new League()
            {
                Id = 2,
                Name = "Serie A",
                Country = "Italia",
                Season = "2017/2018",
                MVP = "Mauro Icardi",
                Winner = "Juventus",
                Rounds = null,
                Teams = null
            },
            new League()
            {
                Id = 3,
                Name = "Lotto Ekstraklasa",
                Country = "Poland",
                Season = "2018/2019",
                MVP = "Igor Angulo",
                Winner = "Piast Gliwice",
                Rounds = null,
                Teams = null
            }};

            IEnumerable<League> testLeagues = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await repository.GetAllAsync());

            // Assert
            Assert.Null(err);
            Assert.NotNull(testLeagues);
            Assert.Equal(expectedleagues.Count(), testLeagues.Count());
            for (int i = 0; i < expectedleagues.Count(); i++)
            {
                Assert.Equal(expectedleagues.ElementAt(i).Id, testLeagues.ElementAt(i).Id);
                Assert.Equal(expectedleagues.ElementAt(i).Shortname, testLeagues.ElementAt(i).Shortname);
                Assert.Equal(expectedleagues.ElementAt(i).Name, testLeagues.ElementAt(i).Name);
                Assert.Equal(expectedleagues.ElementAt(i).Country, testLeagues.ElementAt(i).Country);
                Assert.Equal(expectedleagues.ElementAt(i).Season, testLeagues.ElementAt(i).Season);
                Assert.Equal(expectedleagues.ElementAt(i).MVP, testLeagues.ElementAt(i).MVP);
                Assert.Equal(expectedleagues.ElementAt(i).Winner, testLeagues.ElementAt(i).Winner);
            }

        }
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
