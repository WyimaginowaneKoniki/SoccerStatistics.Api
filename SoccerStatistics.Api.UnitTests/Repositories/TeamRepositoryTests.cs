using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private ITeamRepository _teamRepository;

        public TeamRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _teamRepository = null;
        }

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
            Assert.Null(err);
            Assert.NotNull(testTeams);
            Assert.Equal(expectedTeams.Count(), testTeams.Count());

            for (int i = 0; i < expectedTeams.Count(); i++)
            {
                Assert.True(_compareLogic.Compare(expectedTeams.ElementAt(i), testTeams.ElementAt(i)).AreEqual);
            }
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
            Assert.Null(err);
            Assert.NotNull(testTeam);

            Assert.True(_compareLogic.Compare(expectedTeam, testTeam).AreEqual);
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
            Assert.Null(err);
            Assert.Null(testTeam);
        }
    }
}
