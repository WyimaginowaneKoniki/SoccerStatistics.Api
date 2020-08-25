using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Entities.Enums;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class PlayerRepositoryTests
    {
        private IPlayerRepository _playerRepository;

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            _playerRepository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetAllPlayers");

            IEnumerable<Player> expectedPlayers = new List<Player>
            {
                  new Player()
                {
                    Id = 1,
                    Name = "Lionel",
                    Surname = "Messi",
                    Height = 169,
                    Weight = 68,
                    Birthday = new DateTime(1987, 6, 23),
                    Nationality = "Argentina",
                    DominantLeg = DominantLegType.Left,
                    Nick = "La Pulga",
                    Number = 10
                },

                new Player()
                {
                    Id = 2,
                    Name = "Cristiano",
                    Surname = "Rolando",
                    Height = 189,
                    Weight = 85,
                    Birthday = new DateTime(1985, 2, 5),
                    Nationality = "Portugal",
                    DominantLeg = DominantLegType.Right,
                    Nick = "CR7",
                    Number = 7
                },

                new Player()
                {
                    Id = 3,
                    Name = "Michał",
                    Surname = "Pazdan",
                    Height = 180,
                    Weight = 78,
                    Birthday = new DateTime(1987, 9, 21),
                    Nationality = "Poland",
                    DominantLeg = DominantLegType.Undefined,
                    Nick = "Priest",
                    Number = 22
                }
            };

            IEnumerable<Player> testPlayers = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayers = await _playerRepository.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testPlayers.Should().NotBeNull();

            testPlayers.Should().HaveSameCount(expectedPlayers);

            testPlayers.Should().BeEquivalentTo(expectedPlayers);
        }

        [Fact]
        public async Task ReturnPlayerWhoExistsInDbByGivenId()
        {
            // Arrange
            _playerRepository = SoccerStatisticsContextMocker.GetInMemoryPlayerRepository("GetPlayerByIdReturnPlayer");

            var expectedPlayer = new Player()
            {
                Id = 1,
                Name = "Lionel",
                Surname = "Messi",
                Height = 169,
                Weight = 68,
                Birthday = new DateTime(1987, 6, 23),
                Nationality = "Argentina",
                DominantLeg = DominantLegType.Left,
                Nick = "La Pulga",
                Number = 10
            };

            Player testPlayer = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await _playerRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testPlayer.Should().NotBeNull();

            testPlayer.Should().BeEquivalentTo(expectedPlayer);
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
            err.Should().BeNull();

            testPlayer.Should().BeNull();
        }
    }
}
