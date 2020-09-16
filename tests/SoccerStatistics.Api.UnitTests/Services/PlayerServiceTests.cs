using AutoMapper;
using FluentAssertions;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class PlayerServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IPlayerRepository> _repositoryMock;
        private readonly IPlayerService _service;
        private readonly IFakeData _fakeData;

        public PlayerServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperPlayerProfile>());

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<IPlayerRepository>();
            _service = new PlayerService(_repositoryMock.Object, _mapper);

            _fakeData = new FakeData();
        }

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            var fakePlayers = _fakeData.GetFakePlayer().Generate(3);

            IEnumerable<PlayerDTO> testPlayers = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakePlayers);

            var expectedPlayers = _mapper.Map<IEnumerable<PlayerDTO>>(fakePlayers);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayers = await _service.GetAllAsync());

            // Arrange
            err.Should().BeNull();

            testPlayers.Should().NotBeNull();
            testPlayers.Should().HaveSameCount(expectedPlayers);
            testPlayers.Should().BeEquivalentTo(expectedPlayers);
        }

        [Fact]
        public async void ReturnPlayerWhoExistsInDbByGivenId()
        {
            // Assert
            var fakePlayers = _fakeData.GetFakePlayer().Generate(3);

            PlayerDTO testPlayer = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakePlayers[0]);

            var expectedPlayer = _mapper.Map<PlayerDTO>(fakePlayers[0]);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testPlayer.Should().NotBeNull();
            testPlayer.Should().BeEquivalentTo(expectedPlayer);
        }


        [Fact]
        public async void ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
        {
            // Assert
            PlayerDTO testPlayer = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Player)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testPlayer.Should().BeNull();
        }
    }
}
