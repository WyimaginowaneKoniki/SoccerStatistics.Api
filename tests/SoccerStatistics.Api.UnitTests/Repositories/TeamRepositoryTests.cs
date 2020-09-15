using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests : RepositoryTestBase
    {
        private ITeamRepository _teamRepository;

        [Fact]
        public async Task ReturnAllTeamFromDb()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(3);

            var context = GetInMemory("GetAllTeams", fakeTeams);

            _teamRepository = new TeamRepository(context);

            IEnumerable<Team> testTeams = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _teamRepository.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testTeams.Should().NotBeNull();

            testTeams.Should().HaveSameCount(fakeTeams);
            testTeams.Should().BeEquivalentTo(fakeTeams);
        }

        [Fact]
        public async Task ReturnTeamWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(3);

            var context = GetInMemory("GetTeamByIdReturnTeam", fakeTeams);

            _teamRepository = new TeamRepository(context);

            Team testTeam = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _teamRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testTeam.Should().NotBeNull();

            testTeam.Should().BeEquivalentTo(fakeTeams[0]);
        }

        [Fact]
        public async Task ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(3);

            var context = GetInMemory("GetTeamByIdReturnNull", fakeTeams);

            _teamRepository = new TeamRepository(context);

            Team testTeam = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _teamRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();

            testTeam.Should().BeNull();
        }
    }
}
