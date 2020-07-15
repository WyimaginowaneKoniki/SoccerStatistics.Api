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
                MatchTeam1 = new Team() { Id = 1, FullName = "Manchester United FC" },
                MatchTeam2 = new Team() { Id = 2, FullName = "FC Trampkarze" }
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

            var teamInMatchStatsRepositoryMock = new Mock<ITeamInMatchStatsRepository>();
            teamInMatchStatsRepositoryMock.Setup(r => r.GetAllByMatchIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            teamInMatchStatsRepositoryMock.Setup(r => r.GetAllByMatchIdAsync(1)).ReturnsAsync(teamInMatchStats.AsEnumerable());

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

            var expectedMatch = new MatchDTO()
            {
                Id = match.Id,
                Stadium = mapper.Map<StadiumDTO>(match.Stadium),
                AmountOfFans = match.AmountOfFans,
                Round = mapper.Map<RoundDTO>(match.Round),
                Date = match.Date,
                TeamInMatchStats1 = mapper.Map<TeamInMatchStatsDTO>(teamInMatchStats.ElementAtOrDefault(0)),
                TeamInMatchStats2 = mapper.Map<TeamInMatchStatsDTO>(teamInMatchStats.ElementAtOrDefault(1)),
                Activities = mapper.Map<IEnumerable<ActivityDTO>>(match.Activities),
                InteractionsBetweenPlayers = mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers)
            };

            var service = new MatchService(matchRepositoryMock.Object, teamInMatchStatsRepositoryMock.Object, mapper);

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

            var teamInMatchStatsRepositoryMock = new Mock<ITeamInMatchStatsRepository>();
            teamInMatchStatsRepositoryMock.Setup(r => r.GetAllByMatchIdAsync(It.IsAny<uint>())).ReturnsAsync((IEnumerable<TeamInMatchStats>)null);


            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperTeamInMatchStatsProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
            });

            var mapper = new Mapper(configuration);

            var service = new MatchService(matchRepositoryMock.Object, teamInMatchStatsRepositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await service.GetByIdAsync(125215));
            // Arrange
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
