using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class RoundServiceTests
    {
        [Fact]
        public async void ReturnRoundWhichExistsInDbByGivenId()
        {
            // Assert
            var round = new Round()
            {
                Id = 1,
                Name = "Round 1"
            };

            RoundDTO testRound = null;

            var repositoryMock = new Mock<IRoundRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(round);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperRoundProfile>());

            var mapper = new Mapper(configuration);

            var expectedRound = mapper.Map<RoundDTO>(round);

            var service = new RoundService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testRound);
            Assert.Equal(expectedRound.Id, testRound.Id);
            Assert.Equal(expectedRound.Name, testRound.Name);
     
        }


        [Fact]
        public async void ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Assert
            RoundDTO testRound = null;

            var repositoryMock = new Mock<IRoundRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Round)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperRoundProfile>());

            var mapper = new Mapper(configuration);


            var service = new RoundService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testRound);
        }
    }
}
