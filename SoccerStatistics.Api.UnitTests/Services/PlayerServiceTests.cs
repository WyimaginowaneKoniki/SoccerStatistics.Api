using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
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
                DominantLeg = "Left",
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
            Assert.Null(err);
            Assert.NotNull(testPlayer);

            testPlayer.ShouldCompare(expectedPlayer);
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
            Assert.Null(err);
            Assert.Null(testPlayer);
        }
    }
}
