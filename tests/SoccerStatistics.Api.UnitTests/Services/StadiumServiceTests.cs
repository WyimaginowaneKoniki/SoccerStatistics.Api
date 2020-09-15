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
    public class StadiumServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IStadiumRepository> _stadiumRepositoryMock;
        private readonly IStadiumService _service;
        private readonly IFakeData _fakeData;

        public StadiumServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperStadiumProfile>());

            _mapper = new Mapper(configuration);
            _stadiumRepositoryMock = new Mock<IStadiumRepository>();
            _service = new StadiumService(_stadiumRepositoryMock.Object, _mapper);

            _fakeData = new FakeData();
        }

        [Fact]
        public async void ReturnAllStadiumsWhichExistsInDb()
        {
            // Assert
            var stadiums = _fakeData.GetFakeTeam()
                                    .Generate(3)
                                    .Select(x => x.Stadium);

            IEnumerable<StadiumDTO> testStadiums = null;

            _stadiumRepositoryMock.Reset();
            _stadiumRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stadiums);

            var expectedStadiums = _mapper.Map<IEnumerable<StadiumDTO>>(stadiums);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadiums = await _service.GetAllAsync());

            // Arrange
            err.Should().BeNull();

            testStadiums.Should().NotBeNull();
            testStadiums.Should().HaveSameCount(expectedStadiums);
            testStadiums.Should().BeEquivalentTo(expectedStadiums);
        }

        [Fact]
        public async void ReturnStadiumWhichExistsInDbByGivenId()
        {
            // Assert
            var fakeStadium = _fakeData.GetFakeTeam()
                                       .Generate(1)
                                       .Select(x => x.Stadium)
                                       .Single();

            StadiumDTO testStadium = null;

            _stadiumRepositoryMock.Reset();
            _stadiumRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _stadiumRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeStadium);

            var expectedStadium = _mapper.Map<StadiumDTO>(fakeStadium);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testStadium.Should().NotBeNull();
            testStadium.Should().BeEquivalentTo(expectedStadium);
        }

        [Fact]
        public async void ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Assert
            StadiumDTO testStadium = null;

            _stadiumRepositoryMock.Reset();
            _stadiumRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Stadium)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testStadium.Should().BeNull();
        }
    }
}
