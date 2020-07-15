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
    public class TeamServiceTests
    {
        [Fact]
        public async void ReturnTeamWhichExistsInDbByGivenId()
        {
            // Assert
            var team = new Team()
            {
                Id = 1,
                FullName = "Manchester United Football Club",
                ShortName = "Manchester United",
                City = "Stretford",
                CreatedAt = 1878,
                Coach = "Ole Gunnar Solskjær"
            };

            TeamDTO testTeam = null;

            var repositoryMock = new Mock<ITeamRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(team);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperTeamProfile>());

            var mapper = new Mapper(configuration);

            var expectedTeam = mapper.Map<TeamDTO>(team);

            var service = new TeamService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testTeam);
            Assert.Equal(expectedTeam.FullName, testTeam.FullName);
            Assert.Equal(expectedTeam.ShortName, testTeam.ShortName);
            Assert.Equal(expectedTeam.City, expectedTeam.City);
            Assert.Equal(expectedTeam.CreatedAt, expectedTeam.CreatedAt);
            Assert.Equal(expectedTeam.Coach, expectedTeam.Coach);
        }

        [Fact]
        public async void ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Assert
            TeamDTO testTeam = null;

            var repositoryMock = new Mock<ITeamRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Team)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperTeamProfile>());

            var mapper = new Mapper(configuration);


            var service = new TeamService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testTeam);
        }
    }
}
