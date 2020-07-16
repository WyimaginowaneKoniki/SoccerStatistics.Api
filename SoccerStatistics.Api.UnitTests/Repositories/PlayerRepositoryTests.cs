using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class PlayerRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private IPlayerRepository _playerRepository;

        public PlayerRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _playerRepository = null;
        }

        [Fact]
        public async Task ReturnPlayerWhoExistsInDbByGivenId()
        {
            // Arrange
            _playerRepository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetPlayerByIdReturnPlayer");

            var compareLogic = new CompareLogic();

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
                        () => testPlayer = await _playerRepository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testPlayer);

            Assert.True(_compareLogic.Compare(expectedPlayer, testPlayer).AreEqual);
        }

        [Fact]
        public async Task ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
        {
            // Arrange
            _playerRepository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetPlayerByIdReturnNull");

            Player testPlayer = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await _playerRepository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testPlayer);
        }
    }
}
