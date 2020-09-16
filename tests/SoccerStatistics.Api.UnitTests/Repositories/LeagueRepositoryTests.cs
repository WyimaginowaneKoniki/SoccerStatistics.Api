using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class LeagueRepositoryTests : RepositoryTestBase
    {
        private ILeagueRepository _leagueRepository;

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var expectedLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(3);

            var context = GetInMemory("GetAllLeagues", expectedLeagues);

            _leagueRepository = new LeagueRepository(context);

            IEnumerable<League> testLeagues = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await _leagueRepository.GetAllAsync());

            // Assert          
            err.Should().BeNull();

            testLeagues.Should().NotBeNull();
            testLeagues.Should().HaveSameCount(expectedLeagues);
            testLeagues.Should().BeEquivalentTo(expectedLeagues);
        }

        [Fact]
        public async Task ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var expectedLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(2);

            var context = GetInMemory("GetLeagueByIdReturnLeague", expectedLeagues);

            _leagueRepository = new LeagueRepository(context);

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _leagueRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testLeague.Should().NotBeNull();
            testLeague.Should().BeEquivalentTo(expectedLeagues[0]);
        }

        [Fact]
        public async Task ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var expectedLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(2);

            var context = GetInMemory("GetLeagueByIdReturnNull", expectedLeagues);

            _leagueRepository = new LeagueRepository(context);

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
