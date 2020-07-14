using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class LeagueServiceTests
    {
   
        [Fact]
        public async void ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Assert
            var league = new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = "Lionel Messi",
                Winner = "FC Barcelona",
            };

            LeagueDTO testLeague = null;

            var repositoryMock = new Mock<ILeagueRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(league);
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperLeagueProfile>());

            var mapper = new Mapper(configuration);

            var expectedLeague = mapper.Map<LeagueDTO>(league);

            var service = new LeagueService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testLeague);
            Assert.Equal(expectedLeague.Id, testLeague.Id);
            Assert.Equal(expectedLeague.Name, testLeague.Name);
            Assert.Equal(expectedLeague.Country, testLeague.Country);
            Assert.Equal(expectedLeague.Season, testLeague.Season);
            Assert.Equal(expectedLeague.MVP, testLeague.MVP);
            Assert.Equal(expectedLeague.Winner, testLeague.Winner);
        }


        [Fact]
        public async void ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Assert
            LeagueDTO testLeague = null;

            var repositoryMock = new Mock<ILeagueRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((League)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperLeagueProfile>());

            var mapper = new Mapper(configuration);


            var service = new LeagueService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testLeague);
        }
   
    }
}
