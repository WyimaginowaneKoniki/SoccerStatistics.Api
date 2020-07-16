using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class MatchRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private IMatchRepository _matchRepository;

        public MatchRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _matchRepository = null;
        }

        [Fact]
        public async void ReturnMatchWhicExistsInDbByGivenId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnMatch");

            var expectedMatch = new Match()
            {
                Id = 1,
                Stadium = new Stadium
                {
                    Id = 1,
                    Name = "Old Trafford",
                    Country = "England",
                    City = "Manchester",
                    BuiltAt = 1910,
                    Capacity = 75_797,
                    FieldSize = "105:68",
                    Cost = 151_233M,
                    VipCapacity = 4000,
                    IsForDisabled = true,
                    Lighting = 100_000,
                    Architect = "Archibald Leitch",
                    IsNational = false
                },
                AmountOfFans = 60_123,
                Date = new DateTime(2015, 3, 4)
            };

            Match testMatch = null;

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await _matchRepository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);

            Assert.True(_compareLogic.Compare(expectedMatch, testMatch).AreEqual);
        }

        [Fact]
        public async Task ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnNull");

            Match testMatch = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await _matchRepository.GetByIdAsync(523829857));

            // Assert
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
