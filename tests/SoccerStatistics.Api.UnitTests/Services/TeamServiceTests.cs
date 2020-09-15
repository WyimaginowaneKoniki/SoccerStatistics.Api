using AutoMapper;
using FluentAssertions;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database;
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
        private readonly IMapper _mapper;
        private readonly Mock<ITeamRepository> _teamRepositoryMock;
        private readonly Mock<ITeamInLeagueRepository> _teamInLeagueRepositoryMock;
        private readonly ITeamService _service;
        private readonly IFakeData _fakeData;

        public TeamServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperLeagueProfile>();
            });

            _mapper = new Mapper(configuration);
            _teamRepositoryMock = new Mock<ITeamRepository>();
            _teamInLeagueRepositoryMock = new Mock<ITeamInLeagueRepository>();
            _service = new TeamService(_teamRepositoryMock.Object, _teamInLeagueRepositoryMock.Object, _mapper);

            _fakeData = new FakeData();
        }

        [Fact]
        public async void ReturnAllTeamsFromDb()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(3);

            var expectedTeams = _mapper.Map<IEnumerable<TeamDTO>>(fakeTeams);

            IEnumerable<TeamDTO> testTeams = null;

            _teamRepositoryMock.Reset();
            _teamRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeTeams);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _service.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testTeams.Should().NotBeNull();
            testTeams.Should().HaveSameCount(expectedTeams);
            testTeams.Should().BeEquivalentTo(expectedTeams);
        }

        [Fact]
        public async void ReturnNullCollectionWhenDbDoesNotContainsAnyTeam()
        {
            // Arrange
            IEnumerable<TeamDTO> testTeams = null;

            _teamRepositoryMock.Reset();
            _teamRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync((IEnumerable<Team>)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeams = await _service.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testTeams.Should().BeEmpty();
        }

        [Fact]
        public async void ReturnTeamWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(3);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams.Where(x => x.Id == 1))
                                      .Generate(1)
                                      .First();

            var expectedTeam = _mapper.Map<TeamDTO>(fakeTeams[0]);
            expectedTeam.League = _mapper.Map<LeagueBasicDTO>(fakeLeague);

            TeamDTO testTeam = null;

            _teamRepositoryMock.Reset();
            _teamRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _teamRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeTeams[0]);

            _teamInLeagueRepositoryMock.Reset();
            _teamInLeagueRepositoryMock.Setup(r => r.GetLeagueForTeamAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _teamInLeagueRepositoryMock.Setup(r => r.GetLeagueForTeamAsync(1)).ReturnsAsync(fakeLeague);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _service.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testTeam.Should().NotBeNull();
            testTeam.Should().BeEquivalentTo(expectedTeam);
        }

        [Fact]
        public async void ReturnNullWhenTeamDoNotExistsInDbByGivenId()
        {
            // Arrange
            TeamDTO testTeam = null;

            _teamRepositoryMock.Reset();
            _teamRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Team)null);

            _teamInLeagueRepositoryMock.Reset();
            _teamInLeagueRepositoryMock.Setup(r => r.GetLeagueForTeamAsync(It.IsAny<uint>())).ReturnsAsync((League)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testTeam = await _service.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testTeam.Should().BeNull();
        }
    }
}
