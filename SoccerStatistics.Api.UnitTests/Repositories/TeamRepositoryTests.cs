using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        [Fact]
        public async Task ReturnAllTeamFromDb()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetAllTeams");
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

            IEnumerable<Team> teams = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => teams = await repository.GetAllAsync());

            // Assert
            Assert.Null(err);
            Assert.NotNull(teams);
            Assert.Equal(expectedTeams.Count(), teams.Count());

            for (int i = 0; i < expectedTeams.Count(); i++)
            {
                Assert.Equal(expectedTeams.ElementAt(i).Id, teams.ElementAt(i).Id);
                Assert.Equal(expectedTeams.ElementAt(i).FullName, teams.ElementAt(i).FullName);
                Assert.Equal(expectedTeams.ElementAt(i).ShortName, teams.ElementAt(i).ShortName);
                Assert.Equal(expectedTeams.ElementAt(i).City, teams.ElementAt(i).City);
                Assert.Equal(expectedTeams.ElementAt(i).CreatedAt, teams.ElementAt(i).CreatedAt);
                Assert.Equal(expectedTeams.ElementAt(i).Coach, teams.ElementAt(i).Coach);
            }
        }

        [Fact]
        public async Task ReturnTeamWhichExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetTeamByIdReturnTeam");

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
                        () => testTeam = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testTeam);
            Assert.Equal(expectedTeam.Id, testTeam.Id);
            Assert.Equal(expectedTeam.FullName, testTeam.FullName);
            Assert.Equal(expectedTeam.ShortName, testTeam.ShortName);
            Assert.Equal(expectedTeam.City, testTeam.City);
            Assert.Equal(expectedTeam.CreatedAt, testTeam.CreatedAt);
            Assert.Equal(expectedTeam.Coach, testTeam.Coach);

        }

        [Fact]
        public async Task ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetTeamByIdReturnNull");

            Team testTeam = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await repository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testTeam);
        }
    }
}
