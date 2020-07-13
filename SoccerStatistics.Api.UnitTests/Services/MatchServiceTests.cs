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

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class MatchServiceTests
    {
        [Fact]
        public async void ReturnMatchWhicExistsInDbByGivenId()
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

            var repositoryMock = new Mock<IMatchRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

            var configuration = new MapperConfiguration(cfg
               => cfg.AddProfile<AutoMapperMatchProfile>());

            var mapper = new Mapper(configuration);

            var expectedMatch = mapper.Map<MatchDTO>(match);

            var service = new MatchService(repositoryMock.Object, mapper);

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await service.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);
            Assert.Equal(expectedMatch.StadiumId, testMatch.StadiumId);
            Assert.Equal(expectedMatch.AmountOfFans, testMatch.AmountOfFans);
            Assert.Equal(expectedMatch.Date, testMatch.Date);
            Assert.Equal(expectedMatch.MatchTeam1Id, testMatch.MatchTeam1Id);
            Assert.Equal(expectedMatch.MatchTeam2Id, testMatch.MatchTeam2Id);
        }

        [Fact]
        public async void ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Assert
            MatchDTO testMatch = null;

            var repositoryMock = new Mock<IMatchRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Database.Entities.Match)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperMatchProfile>());

            var mapper = new Mapper(configuration);

            var service = new MatchService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
