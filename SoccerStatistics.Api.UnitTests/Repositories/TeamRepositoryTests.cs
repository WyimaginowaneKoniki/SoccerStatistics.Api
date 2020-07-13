using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class TeamRepositoryTests
    {
        [Fact]
        public async Task ReturnTeamWhoExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryTeamRepository("GetTeamByIdReturnTeam");

            var expectedTeam = new Team()
            {
                Id = 1,
                FullName = "Manchester United Football Club",
                ShortName = "Manchester United",
                City = "Stretford",
                CreatedAt = new DateTime(1878, 1, 1),
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
        public async Task ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
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
