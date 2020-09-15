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
            _context = GetInMemory("GetAllPlayers");

            _playerRepository = new PlayerRepository(_context);

            var expectedPlayers = _fakeData.GetFakePlayer().Generate(5);

            _context.AddRange(expectedPlayers);
            _context.SaveChanges();

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
            _context = GetInMemory("GetPlayerById");

            _playerRepository = new PlayerRepository(_context);

            var fakePlayers = _fakeData.GetFakePlayer().Generate(5);

            _context.AddRange(fakePlayers);
            _context.SaveChanges();

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
            _context = GetInMemory("GetPlayerByIdReturnNull");

            _playerRepository = new PlayerRepository(_context);

            var fakePlayers = _fakeData.GetFakePlayer().Generate(2);

            _context.AddRange(fakePlayers);
            _context.SaveChanges();

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
