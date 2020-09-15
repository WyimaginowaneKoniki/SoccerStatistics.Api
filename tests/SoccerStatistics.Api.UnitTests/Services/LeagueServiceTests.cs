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
using System.Configuration;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class LeagueServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeagueRepository> _repositoryMock;
        private readonly ILeagueService _service;
        private readonly IFakeData _fakeData;

        public LeagueServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperLeagueProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperPlayerProfile>();
            });

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<ILeagueRepository>();
            _service = new LeagueService(_repositoryMock.Object, _mapper);

            _fakeData = new FakeData();
        }

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            var fakeTeams = _fakeData.GetFakeTeam().Generate(12);
            var fakeLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(3);

            IEnumerable<LeagueDTO> testLeagues = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(fakeLeagues);

            var expectedLeagues = _mapper.Map<IEnumerable<LeagueDTO>>(fakeLeagues);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await _service.GetAllAsync());

            // Arrange
            err.Should().BeNull();

            testLeagues.Should().NotBeNull();
            testLeagues.Should().HaveSameCount(expectedLeagues);
            testLeagues.Should().BeEquivalentTo(expectedLeagues);
        }

        [Fact]
        public async void ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Assert
            var fakeTeams = _fakeData.GetFakeTeam().Generate(12);
            var fakeLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(3);

            LeagueDTO testLeague = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeLeagues[0]);

            var expectedLeague = _mapper.Map<LeagueDTO>(fakeLeagues[0]);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testLeague.Should().NotBeNull();
            testLeague.Should().BeEquivalentTo(expectedLeague);
        }


        [Fact]
        public async void ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Assert
            LeagueDTO testLeague = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((League)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testLeague.Should().BeNull();
        }
    }
}
