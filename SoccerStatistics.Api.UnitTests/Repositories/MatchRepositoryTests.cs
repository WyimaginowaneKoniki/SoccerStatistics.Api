using SoccerStatistics.Api.Core.DTO;
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
        [Fact]
        public async void ReturnMatchWhicExistsInDbByGivenId()
        {
            // Arange
            IMatchRepository repository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnMatch");
            var expectedMatch = new Database.Entities.Match()
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
                Date = new DateTime(2015, 3, 4),
                MatchTeam1 = new Team() { Id = 1, FullName = "Manchester United FC" },
                MatchTeam2 = new Team() { Id = 2, FullName = "FC Trampkarze" }
            };

            Match testMatch = null;

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);
            Assert.Equal(expectedMatch.Stadium.Name, testMatch.Stadium.Name);
            Assert.Equal(expectedMatch.Stadium.Country, testMatch.Stadium.Country);
            Assert.Equal(expectedMatch.Stadium.City, testMatch.Stadium.City);
            Assert.Equal(expectedMatch.Stadium.BuiltAt, testMatch.Stadium.BuiltAt);
            Assert.Equal(expectedMatch.Stadium.Capacity, testMatch.Stadium.Capacity);
            Assert.Equal(expectedMatch.Stadium.FieldSize, testMatch.Stadium.FieldSize);
            Assert.Equal(expectedMatch.Stadium.Cost, testMatch.Stadium.Cost);
            Assert.Equal(expectedMatch.Stadium.VipCapacity, testMatch.Stadium.VipCapacity);
            Assert.Equal(expectedMatch.Stadium.IsForDisabled, testMatch.Stadium.IsForDisabled);
            Assert.Equal(expectedMatch.Stadium.Lighting, testMatch.Stadium.Lighting);
            Assert.Equal(expectedMatch.Stadium.Architect, testMatch.Stadium.Architect);
            Assert.Equal(expectedMatch.Stadium.IsNational, testMatch.Stadium.IsNational);
            Assert.Equal(expectedMatch.AmountOfFans, testMatch.AmountOfFans);
            Assert.Equal(expectedMatch.Date, testMatch.Date);
            Assert.Equal(expectedMatch.MatchTeam1.FullName, testMatch.MatchTeam1.FullName);
            Assert.Equal(expectedMatch.MatchTeam2.FullName, testMatch.MatchTeam2.FullName);
        }

        [Fact]
        public async Task ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnNull");

            Match testMatch = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await repository.GetByIdAsync(523829857));

            // Assert
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
