using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class TeamServiceTests
    {
        [Fact]
        public async void ReturnAllTeamsFromDb()
        {
            IEnumerable<Team> returnedTeams = new List<Team>
            {
                new Team()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City = "Stretford",
                    CreatedAt = 1878,
                    Coach = "Ole Gunnar Solskjær"
                },
                new Team()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid",
                    CreatedAt = 1902,
                    Coach = "Zinedine Zidane"
                },
                new Team()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona",
                    CreatedAt = 1899,
                    Coach = "Quique Setien"
                }
            };

            IEnumerable<TeamBasicDTO> expectedTeams = new List<TeamBasicDTO>()
            {
                new TeamBasicDTO()
                {
                    Id = 1,
                    FullName = "Manchester United Football Club",
                    ShortName = "Manchester United",
                    City = "Stretford"
                },
                new TeamBasicDTO()
                {
                    Id = 2,
                    FullName = "Real Madrid Club de Futbol",
                    ShortName = "Real Madrid",
                    City = "Madrid"
                },
                new TeamBasicDTO()
                {
                    Id = 3,
                    FullName = "Futbol Club Barcelona",
                    ShortName = "FC Barcelona",
                    City = "Barcelona"
                }
            };

            IEnumerable<TeamBasicDTO> teams = null;


            var repositoryMock = new Mock<ITeamRepository>();
            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(returnedTeams);

            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperTeamProfile>());

            var mapper = new Mapper(configuration);

            var service = new TeamService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => teams = await service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(teams);
            Assert.Equal(expectedTeams.Count(), teams.Count());

            for (int i = 0; i < expectedTeams.Count(); i++)
            {
                Assert.Equal(expectedTeams.ElementAt(i).Id, teams.ElementAt(i).Id);
                Assert.Equal(expectedTeams.ElementAt(i).FullName, teams.ElementAt(i).FullName);
                Assert.Equal(expectedTeams.ElementAt(i).ShortName, teams.ElementAt(i).ShortName);
                Assert.Equal(expectedTeams.ElementAt(i).City, teams.ElementAt(i).City);
            }
        }

        [Fact]
        public async void ReturnNullCollectionWhenDbDoesNotContainsAnyTeam()
        {
            IEnumerable<TeamBasicDTO> teams = null;

            var repositoryMock = new Mock<ITeamRepository>();
            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Team>)null);

            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperTeamProfile>());

            var mapper = new Mapper(configuration);

            var service = new TeamService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => teams = await service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.Equal(Enumerable.Empty<TeamBasicDTO>(),  teams);
        }

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
