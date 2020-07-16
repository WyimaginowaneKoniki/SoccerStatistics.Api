using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using Xunit;
using Match = SoccerStatistics.Api.Database.Entities.Match;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class MatchServiceTests
    {
        private readonly CompareLogic _compareLogic;
        private readonly IMapper _mapper;
        private readonly Mock<IMatchRepository> _repositoryMock;
        private readonly IMatchService _service;

        public MatchServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperMatchProfile>());

            _mapper = new Mapper(configuration);
            _compareLogic = new CompareLogic();
            _repositoryMock = new Mock<IMatchRepository>();
            _service = new MatchService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnMatchWhicExistsInDbByGivenId()
        {
            // Arrange
            var match = new Match()
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

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

            var expectedMatch = _mapper.Map<MatchDTO>(match);

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await _service.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);

            Assert.True(_compareLogic.Compare(expectedMatch, testMatch).AreEqual);
        }

        [Fact]
        public async void ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Assert
            MatchDTO testMatch = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Match)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
