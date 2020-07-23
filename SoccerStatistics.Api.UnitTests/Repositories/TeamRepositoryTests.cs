using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        private ITeamRepository _teamRepository;

        [Fact]
        public async Task ReturnAllTeamFromDb()
        {
            // Arrange
            _teamRepository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetAllTeams");

            IEnumerable<Team> expectedTeams = new List<Team>
            {
                new Team()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City = "Stretford",
                    CreatedAt = 1878,
                    Coach = "Ole Gunnar Solskjær"
                },
                new Team()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    CreatedAt = 1902,
                    Coach = "Zinedine Zidane"
                },
                new Team()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    CreatedAt = 1899,
                    Coach = "Quique Setien"
                }
            };

            IEnumerable<Team> testTeams = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _teamRepository.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testTeams.Should().NotBeNull();

            testTeams.Should().HaveSameCount(expectedTeams);

            testTeams.Should().BeEquivalentTo(expectedTeams);
        }

        [Fact]
        public async Task ReturnTeamWhichExistsInDbByGivenId()
        {
            // Arrange
            _teamRepository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetTeamByIdReturnTeam");

            var expectedTeam = new Team()
            {
                Id = 1,
                FullName = "Manchester United Football Club",
                ShortName = "Manchester United",
                City = "Stretford",
                CreatedAt = 1878,
                Coach = "Ole Gunnar Solskjær"
            };

            Team testTeam = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _teamRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testTeam.Should().NotBeNull();

            testTeam.Should().BeEquivalentTo(expectedTeam);
        }

        [Fact]
        public async Task ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Arrange
            _teamRepository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetTeamByIdReturnNull");

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
