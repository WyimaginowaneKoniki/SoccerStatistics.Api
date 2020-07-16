using KellermanSoftware.CompareNetObjects;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class StadiumRepositoryTests
    {
        private readonly CompareLogic _compareLogic;
        private IStadiumRepository _stadiumRepository;

        public StadiumRepositoryTests()
        {
            _compareLogic = new CompareLogic();
            _stadiumRepository = null;
        }

        [Fact]
        public async Task ReturnAllStadiumsWhichExistsInDb()
        {
            // Arrange
            _stadiumRepository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetAllStadiums");

            IEnumerable<Stadium> expectedstadiums = new List<Stadium>
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

            IEnumerable<Stadium> testStadiums = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => testStadiums = await _stadiumRepository.GetAllAsync());

            // Assert
            Assert.Null(err);
            Assert.NotNull(testStadiums);
            Assert.Equal(expectedstadiums.Count(), testStadiums.Count());
            for (int i = 0; i < expectedstadiums.Count(); i++)
            {
                Assert.True(_compareLogic.Compare(expectedstadiums.ElementAt(i), testStadiums.ElementAt(i)).AreEqual);
            }

        }

        [Fact]
        public async Task ReturnStadiumWhichExistsInDbByGivenId()
        {
            // Arrange
            _stadiumRepository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetStadiumByIdReturnStadium");

            var expectedStadium = new Stadium()
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

            Stadium testStadium = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _stadiumRepository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testStadium);

            Assert.True(_compareLogic.Compare(expectedStadium, testStadium).AreEqual);
        }

        [Fact]
        public async Task ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Arrange
            _stadiumRepository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetStadiumByIdReturnNull");

            Stadium testStadium = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _stadiumRepository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testStadium);
        }
    }
}
