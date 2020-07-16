using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RoundRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private IRoundRepository _roundRepository;

        public RoundRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _roundRepository = null;
        }

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
            Assert.Null(err);
            Assert.NotNull(testRound);

            Assert.True(_compareLogic.Compare(expectedRound, testRound).AreEqual);
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
            Assert.Null(err);
            Assert.Null(testRound);
        }
    }
}
