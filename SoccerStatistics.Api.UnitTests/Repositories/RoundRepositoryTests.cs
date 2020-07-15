using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RoundRepositoryTests
    {
        [Fact]
        public async Task ReturnRoundWhichExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryRoundRepository("GetRoundByIdReturnRound");

            var expectedRound = new Round()
            {
                Id = 1,
                Name = "Round 1",
            };

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testRound);
            Assert.Equal(expectedRound.Id, testRound.Id);
            Assert.Equal(expectedRound.Name, testRound.Name);
        }

        [Fact]
        public async Task ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryRoundRepository("GetRoundByIdReturnNull");

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await repository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testRound);
        }
    }
}
