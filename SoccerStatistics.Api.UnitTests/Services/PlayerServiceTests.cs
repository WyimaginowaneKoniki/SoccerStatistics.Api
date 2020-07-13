using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class PlayerServiceTests
    {
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

            var repositoryMock = new Mock<IPlayerRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(player);
            

            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperPlayerProfile>());

            var mapper = new Mapper(configuration);

            var expectedPlayer = mapper.Map<PlayerDTO>(player);

            var service = new PlayerService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testPlayer);
            Assert.Equal(expectedPlayer.Name, testPlayer.Name);
            Assert.Equal(expectedPlayer.Surname, testPlayer.Surname);
            Assert.Equal(expectedPlayer.Height, testPlayer.Height);
            Assert.Equal(expectedPlayer.Weight, testPlayer.Weight);
            Assert.Equal(expectedPlayer.Birthday, testPlayer.Birthday);
            Assert.Equal(expectedPlayer.Nationality, testPlayer.Nationality);
            Assert.Equal(expectedPlayer.DominantLeg, testPlayer.DominantLeg);
            Assert.Equal(expectedPlayer.Nick, testPlayer.Nick);
            Assert.Equal(expectedPlayer.Number, testPlayer.Number);
        }


        [Fact]
        public async void ReturnNullWhenPlayerDoNotExistsInDbByGivenId()
        {
            // Assert
            PlayerDTO testPlayer = null;

            var repositoryMock = new Mock<IPlayerRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Player)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperPlayerProfile>());

            var mapper = new Mapper(configuration);


            var service = new PlayerService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testPlayer = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testPlayer);
        }
    }
}
