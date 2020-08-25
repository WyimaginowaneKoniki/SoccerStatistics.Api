using AutoMapper;
using FluentAssertions;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Entities.Enums;
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

        public PlayerServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperPlayerProfile>());

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<IPlayerRepository>();
            _service = new PlayerService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            IEnumerable<Player> players = new List<Player>
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

            IEnumerable<PlayerDTO> testPlayers = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(players);

            var expectedPlayers = _mapper.Map<IEnumerable<PlayerDTO>>(players);

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
            var player = new Player()
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

            PlayerDTO testPlayer = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);

            var expectedPlayer = _mapper.Map<PlayerDTO>(player);

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
