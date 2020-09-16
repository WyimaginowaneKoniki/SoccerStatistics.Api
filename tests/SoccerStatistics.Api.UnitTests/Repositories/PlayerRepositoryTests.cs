using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class PlayerRepositoryTests : RepositoryTestBase
    {
        private IPlayerRepository _playerRepository;

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            var fakePlayers = _fakeData.GetFakePlayer().Generate(5);

            var context = GetInMemory("GetAllPlayers", fakePlayers);

            _playerRepository = new PlayerRepository(context);

            IEnumerable<Player> testPlayers = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayers = await _playerRepository.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testPlayers.Should().NotBeNull();
            testPlayers.Should().HaveSameCount(fakePlayers);
            testPlayers.Should().BeEquivalentTo(fakePlayers);
        }

        [Fact]
        public async Task ReturnPlayerWhoExistsInDbByGivenId()
        {
            // Arrange
            var fakePlayers = _fakeData.GetFakePlayer().Generate(5);

            var context = GetInMemory("GetPlayerById", fakePlayers);

            _playerRepository = new PlayerRepository(context);

            Player testPlayer = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await _playerRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testPlayer.Should().NotBeNull();
            testPlayer.Should().BeEquivalentTo(fakePlayers[0]);
        }

        [Fact]
        public async Task ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakePlayers = _fakeData.GetFakePlayer().Generate(2);

            var context = GetInMemory("GetPlayerByIdReturnNull", fakePlayers);

            _playerRepository = new PlayerRepository(context);

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
