using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RoundRepositoryTests
    {
        private IRoundRepository _roundRepository;

        [Fact]
        public async Task ReturnRoundWhichExistsInDbByGivenId()
        {
            // Arrange
            _roundRepository = SoccerStatisticsContextMocker.GetInMemoryRoundRepository("GetRoundByIdReturnRound");

            var expectedRound = new Round()
            {
                Id = 1,
                Name = "Round 1",
            };

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _roundRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testRound.Should().NotBeNull();

            testRound.Should().BeEquivalentTo(expectedRound);
        }

        [Fact]
        public async Task ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Arrange
            _roundRepository = SoccerStatisticsContextMocker.GetInMemoryRoundRepository("GetRoundByIdReturnNull");

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _roundRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();

            testRound.Should().BeNull();
        }
    }
}
