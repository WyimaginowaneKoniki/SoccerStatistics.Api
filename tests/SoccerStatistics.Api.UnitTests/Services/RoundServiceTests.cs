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
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class RoundServiceTests
    {
        private readonly IMapper _mapper;
        private readonly Mock<IRoundRepository> _repositoryMock;
        private readonly IRoundService _service;

        public RoundServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperRoundProfile>());

            _mapper = new Mapper(configuration);
            _repositoryMock = new Mock<IRoundRepository>();
            _service = new RoundService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnRoundWhichExistsInDbByGivenId()
        {
            // Assert
            var round = new Round()
            {
                Id = 1,
                Name = "Round 1"
            };

            RoundDTO testRound = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(round);

            var expectedRound = _mapper.Map<RoundDTO>(round);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testRound.Should().NotBeNull();

            testRound.Should().BeEquivalentTo(expectedRound);
        }

        [Fact]
        public async void ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Assert
            RoundDTO testRound = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Round)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _service.GetByIdAsync(1));

            // Arrange
            err.Should().BeNull();

            testRound.Should().BeNull();
        }
    }
}
