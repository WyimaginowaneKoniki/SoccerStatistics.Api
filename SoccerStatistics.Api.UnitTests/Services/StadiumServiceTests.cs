using AutoMapper;
using KellermanSoftware.CompareNetObjects;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class StadiumServiceTests
    {
        private readonly CompareLogic _compareLogic;
        private readonly IMapper _mapper;
        private readonly Mock<IStadiumRepository> _repositoryMock;
        private readonly IStadiumService _service;

        public StadiumServiceTests()
        {
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperStadiumProfile>());

            _mapper = new Mapper(configuration);
            _compareLogic = new CompareLogic();
            _repositoryMock = new Mock<IStadiumRepository>();
            _service = new StadiumService(_repositoryMock.Object, _mapper);
        }

        [Fact]
        public async void ReturnAllStadiumsWhichExistsInDb()
        {
            IEnumerable<Stadium> stadiums = new List<Stadium>
            {

                new Stadium
                 {
                     Id = 1,
                     Name = "Old Trafford",
                     Country = "England",
                     City = "Manchester",
                     BuiltAt = 1910,
                     Capacity = 75_797,
                     FieldSize = "105:68",
                     VipCapacity = 4000,
                     IsForDisabled = true,
                     Lighting = 100_000,
                     Architect = "Archibald Leitch",
                     IsNational = false
                 },

               new Stadium
               {
                   Id = 2,
                   Name = "Camp Nou",
                   Country = "Spain",
                   City = "Barcelona",
                   BuiltAt = 1957,
                   Capacity = 99_354,
                   FieldSize = "105:68",
                   VipCapacity = 400,
                   IsForDisabled = false,
                   Lighting = 1770,
                   Architect = "Francesc Mijtans-Miro",
                   IsNational = false
               },

               new Stadium
               {
                   Id = 3,
                   Name = "Estadio Nacional de Brasilia Mane Garrincha",
                   Country = "Brasilia",
                   City = "Brasilia",
                   BuiltAt = 2013,
                   Capacity = 72_888,
                   FieldSize = "105:68",
                   VipCapacity = 110,
                   IsForDisabled = true,
                   Lighting = 1770,
                   Architect = "Castro Mello Architects",
                   IsNational = true
               }};

            IEnumerable<StadiumDTO> testStadiums = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(stadiums);

            var expectedStadium = _mapper.Map<IEnumerable<StadiumDTO>>(stadiums);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadiums = await _service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testStadiums);
            Assert.Equal(expectedStadium.Count(), testStadiums.Count());

            for (int i = 0; i < expectedStadium.Count(); i++)
            {
                Assert.True(_compareLogic.Compare(expectedStadium.ElementAt(i), testStadiums.ElementAt(i)).AreEqual);
            }


        }

        [Fact]
        public async void ReturnStadiumWhichExistsInDbByGivenId()
        {
            // Assert
            var stadium = new Stadium()
            {
                Id = 1,
                Name = "Old Trafford",
                Country = "England",
                City = "Manchester",
                BuiltAt = 1910,
                Capacity = 75_797,
                FieldSize = "105:68",
                VipCapacity = 4000,
                IsForDisabled = true,
                Lighting = 100_000,
                Architect = "Archibald Leitch",
                IsNational = false
            };

            StadiumDTO testStadium = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(stadium);

            var expectedStadium = _mapper.Map<StadiumDTO>(stadium);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testStadium);

            Assert.True(_compareLogic.Compare(expectedStadium, testStadium).AreEqual);
        }


        [Fact]
        public async void ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Assert
            StadiumDTO testStadium = null;

            _repositoryMock.Reset();
            _repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Stadium)null);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testStadium);
        }
    }
}
