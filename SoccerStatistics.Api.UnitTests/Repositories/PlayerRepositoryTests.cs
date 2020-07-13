using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class PlayerRepositoryTests
    {
        [Fact]
        public async Task ReturnPlayerWhoExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetPlayerByIdReturnPlayer");

            var expectedPlayer = new Player()
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Height = 169,
                Weight = 68,
                Birthday = new DateTime(1987, 6, 23),
                Nationality = "Argentina",
                DominantLeg = "Left",
                Nick = "La Pulga",
                Number = 10
            };

            Player testPlayer = null;

            // Act
            var err = await Record.ExceptionAsync(async 
                        () => testPlayer = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testPlayer);
            Assert.Equal(expectedPlayer.Id, testPlayer.Id);
            Assert.Equal(expectedPlayer.Name, testPlayer.Name);
            Assert.Equal(expectedPlayer.Surname, testPlayer.Surname);
            Assert.Equal(expectedPlayer.Height, testPlayer.Height);
            Assert.Equal(expectedPlayer.Weight, testPlayer.Weight);
            Assert.Equal(expectedPlayer.Birthday, testPlayer.Birthday);
            Assert.Equal(expectedPlayer.Nationality, testPlayer.Nationality);
            Assert.Equal(expectedPlayer.DominantLeg, testPlayer.DominantLeg);
            Assert.Equal(expectedPlayer.Nick, testPlayer.Nick);
            Assert.Equal(expectedPlayer.Number, testPlayer.Number);
        }

        [Fact]
        public async Task ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetPlayerByIdReturnNull");

            Player testPlayer = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await repository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testPlayer);
        }
    }
}
