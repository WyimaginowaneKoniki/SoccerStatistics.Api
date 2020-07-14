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
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class LeagueServiceTests
    {

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            IEnumerable<League> expectedleagues = new List<League>
            {

                new League()
                {
                  Id = 1,
                  Name = "Primera Division",
                  Country = "Spain",
                  Season = "2018/2019",
                  MVP = "Lionel Messi",
                  Winner = "FC Barcelona",
                  Rounds = null,
                  Teams = null
                  },
            new League()
            {
                Id = 2,
                Name = "Serie A",
                Country = "Italia",
                Season = "2017/2018",
                MVP = "Mauro Icardi",
                Winner = "Juventus",
                Rounds = null,
                Teams = null
            },
            new League()
            {
                Id = 3,
                Name = "Lotto Ekstraklasa",
                Country = "Poland",
                Season = "2018/2019",
                MVP = "Igor Angulo",
                Winner = "Piast Gliwice",
                Rounds = null,
                Teams = null
            }};

            IEnumerable<LeagueDTO> testLeagues = null;


            var repositoryMock = new Mock<ILeagueRepository>();
            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedleagues);
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperLeagueProfile>());

            var mapper = new Mapper(configuration);

            var expectedLeague = mapper.Map<IEnumerable<LeagueDTO>>(expectedleagues);

            var service = new LeagueService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testLeagues);
            Assert.Equal(expectedleagues.Count(), testLeagues.Count());
            for (int i = 0; i < expectedLeague.Count(); i++)
            {
                Assert.Equal(expectedleagues.ElementAt(i).Id, testLeagues.ElementAt(i).Id);
                Assert.Equal(expectedleagues.ElementAt(i).Shortname, testLeagues.ElementAt(i).Shortname);
                Assert.Equal(expectedleagues.ElementAt(i).Name, testLeagues.ElementAt(i).Name);
                Assert.Equal(expectedleagues.ElementAt(i).Country, testLeagues.ElementAt(i).Country);
                Assert.Equal(expectedleagues.ElementAt(i).Season, testLeagues.ElementAt(i).Season);
                Assert.Equal(expectedleagues.ElementAt(i).MVP, testLeagues.ElementAt(i).MVP);
                Assert.Equal(expectedleagues.ElementAt(i).Winner, testLeagues.ElementAt(i).Winner);
            }
          
    
        }
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
