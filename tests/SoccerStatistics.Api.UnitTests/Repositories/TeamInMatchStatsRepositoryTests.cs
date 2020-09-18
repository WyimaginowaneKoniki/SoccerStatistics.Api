using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamInMatchStatsRepositoryTests : RepositoryTestBase
    {
        private ITeamInMatchStatsRepository _teamInMatchStatsRepository;

        [Fact]
        public async Task ReturnTeamInMatchStatsWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeMatch = _fakeData.GetFakeMatch(fakeTeams).Generate(1);

            var context = GetInMemory("GetTeamInMatchStatsById", fakeMatch);

            _teamInMatchStatsRepository = new TeamInMatchStatsRepository(context);

            TeamInMatchStats testTeamInMatchStats = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeamInMatchStats = await _teamInMatchStatsRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testTeamInMatchStats.Should().NotBeNull();
            testTeamInMatchStats.Should().BeEquivalentTo(fakeMatch[0].TeamOneStats);
        }

        [Fact]
        public async Task ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeMatch = _fakeData.GetFakeMatch(fakeTeams).Generate(1);

            var context = GetInMemory("GetTeamInMatchStatsByIdNull", fakeMatch);

            _teamInMatchStatsRepository = new TeamInMatchStatsRepository(context);

            TeamInMatchStats testTeamInMatchStats = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeamInMatchStats = await _teamInMatchStatsRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();
            testTeamInMatchStats.Should().BeNull();
        }
    }
}
