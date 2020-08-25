using AutoMapper;
using FluentAssertions;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class LeagueServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<ILeagueRepository> _repositoryMock;
        private readonly ILeagueService _service;

        public LeagueServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperLeagueProfile>());

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<ILeagueRepository>();
            _service = new LeagueService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnAllLeaguesWhichExistsInDb()
        {
            IEnumerable<League> leagues = new List<League>
            {
                new League()
                {
                    Id = 1,
                    Name = "Primera Division",
                    Country = "Spain",
                    Season = "2018/2019",
                    MVP = new Player() {Name =  "Lionel", Surname = "Messi" },
                    Winner = new Team() {ShortName = "FC Barcelona" },
                    Rounds = null,
                    Teams = null
                },
                new League()
                {
                    Id = 2,
                    Name = "Serie A",
                    Country = "Italia",
                    Season = "2017/2018",
                    MVP = new Player() {Name =  "Mauro", Surname = "Icardi" },
                    Winner = new Team() {ShortName = "Juventus" },
                    Rounds = null,
                    Teams = null
                },
                new League()
                {
                    Id = 3,
                    Name = "Lotto Ekstraklasa",
                    Country = "Poland",
                    Season = "2018/2019",
                    MVP = new Player() {Name =  "Igor", Surname = "Angulo" },
                    Winner = new Team() {ShortName = "Piast Gliwice" },
                    Rounds = null,
                    Teams = null
                }
            };

            IEnumerable<LeagueDTO> testLeagues = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(leagues);

            var expectedLeagues = _mapper.Map<IEnumerable<LeagueDTO>>(leagues);

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
            var league = new League()
            {
                Id = 1,
                Name = "Primera Division",
                Country = "Spain",
                Season = "2018/2019",
                MVP = new Player() { Name = "Lionel", Surname = "Messi" },
                Winner = new Team() { ShortName = "FC Barcelona" }
            };

            LeagueDTO testLeague = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(league);

            var expectedLeague = _mapper.Map<LeagueDTO>(league);

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
