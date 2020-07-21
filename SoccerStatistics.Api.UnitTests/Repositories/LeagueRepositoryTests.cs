using FluentAssertions;
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
        private ILeagueRepository _leagueRepository;

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
            err.Should().BeNull();
           
            testLeagues.Should().NotBeNull();

            testLeagues.Count().Should().Be(expectedleagues.Count());

            testLeagues.Should().BeEquivalentTo(expectedleagues);
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
            err.Should().BeNull();

            testLeague.Should().NotBeNull();

            testLeague.Should().BeEquivalentTo(expectedleague);
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
            err.Should().BeNull();
            testLeague.Should().BeNull();
        }
    }
}
