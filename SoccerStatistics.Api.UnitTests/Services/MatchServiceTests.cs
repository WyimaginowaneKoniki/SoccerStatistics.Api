using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using SoccerStatistics.Api.Core.Services;
using System.Linq;
using KellermanSoftware.CompareNetObjects;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class MatchServiceTests
    {
        [Fact]
        public async void ReturnMatchWhichExistsInDbByGivenId()
        {
            // Arrange
            var match = new Database.Entities.Match()
            {
                Id = 1,
                Stadium = new Stadium
                {
                    Id = 1,
                    Name = "Old Trafford",
                    Country = "England",
                    City = "Manchester",
                    BuiltAt = 1910,
                    Capacity = 75_797,
                    FieldSize = "105:68",
                    Cost = 151_233M,
                    VipCapacity = 4000,
                    IsForDisabled = true,
                    Lighting = 100_000,
                    Architect = "Archibald Leitch",
                    IsNational = false
                },
                AmountOfFans = 60_123,
                Date = new DateTime(2015, 3, 4),
            };

            MatchDTO testMatch = null;

            List<TeamInMatchStats> teamInMatchStats = new List<TeamInMatchStats>()
            {
                new TeamInMatchStats()
                {
                    Id = 1,
                    Pass = 20,
                    Match = match
                },
                new TeamInMatchStats()
                {
                    Id = 2,
                    Pass = 40,
                    Match = match
                }
            };

            var matchRepositoryMock = new Mock<IMatchRepository>();
            matchRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            matchRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperTeamInMatchStatsProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
                cfg.AddProfile<AutoMapperInteractionsBetweenPlayers>();
            });

            var mapper = new Mapper(configuration);

            var expectedMatch = mapper.Map<MatchDTO>(match); 

            var service = new MatchService(matchRepositoryMock.Object, mapper);

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await service.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);

            var compareLogic = new CompareLogic();
            Assert.True(compareLogic.Compare(expectedMatch, testMatch).AreEqual);
        }

        [Fact]
        public async void ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Assert
            MatchDTO testMatch = null;

            var matchRepositoryMock = new Mock<IMatchRepository>();
            matchRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Database.Entities.Match)null);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperTeamInMatchStatsProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
            });

            var mapper = new Mapper(configuration);

            var service = new MatchService(matchRepositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await service.GetByIdAsync(125215));
            // Arrange
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
