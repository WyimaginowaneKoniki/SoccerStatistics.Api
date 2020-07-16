using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class LeagueRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private ILeagueRepository _leagueRepository;

        public LeagueRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _leagueRepository = null;
        }

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            _leagueRepository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetAllLeagues");

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
                        () => testLeagues = await _leagueRepository.GetAllAsync());
            
            // Assert
            Assert.Null(err);
            Assert.NotNull(testLeagues);
            Assert.Equal(expectedleagues.Count(), testLeagues.Count());

            for (int i = 0; i < expectedleagues.Count(); i++)
            {
                Assert.True(_compareLogic.Compare(expectedleagues.ElementAt(i), testLeagues.ElementAt(i)).AreEqual);
            }

        }

        [Fact]
        public async Task ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Arrange
            _leagueRepository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetLeagueByIdReturnLeague");

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
                        () => testLeague = await _leagueRepository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testLeague);
            Assert.True(_compareLogic.Compare(expectedleague, testLeague).AreEqual);
        }

        [Fact]
        public async Task ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Arrange
            _leagueRepository = SoccerStatisticsContextMocker.GetInMemoryLeagueRepository("GetLeagueByIdReturnNull");

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _leagueRepository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testLeague);
        }
    }
}
