using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
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
