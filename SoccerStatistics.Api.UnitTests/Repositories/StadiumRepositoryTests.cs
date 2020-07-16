using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class StadiumRepositoryTests
    {

        [Fact]
        public async Task ReturnAllStadiumsWhichExistsInDb()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetAllStadiums");
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
                        () => testStadiums = await repository.GetAllAsync());

            // Assert
            Assert.Null(err);
            Assert.NotNull(testStadiums);
            Assert.Equal(expectedstadiums.Count(), testStadiums.Count());
            for (int i = 0; i < expectedstadiums.Count(); i++)
            {
                Assert.Equal(expectedstadiums.ElementAt(i).Id, testStadiums.ElementAt(i).Id);
                Assert.Equal(expectedstadiums.ElementAt(i).Name, testStadiums.ElementAt(i).Name);
                Assert.Equal(expectedstadiums.ElementAt(i).Country, testStadiums.ElementAt(i).Country);
                Assert.Equal(expectedstadiums.ElementAt(i).City, testStadiums.ElementAt(i).City);
                Assert.Equal(expectedstadiums.ElementAt(i).Capacity, testStadiums.ElementAt(i).Capacity);
                Assert.Equal(expectedstadiums.ElementAt(i).VipCapacity, testStadiums.ElementAt(i).VipCapacity);
                Assert.Equal(expectedstadiums.ElementAt(i).Architect, testStadiums.ElementAt(i).Architect);
                Assert.Equal(expectedstadiums.ElementAt(i).BuiltAt, testStadiums.ElementAt(i).BuiltAt);
                Assert.Equal(expectedstadiums.ElementAt(i).IsForDisabled, testStadiums.ElementAt(i).IsForDisabled);
                Assert.Equal(expectedstadiums.ElementAt(i).IsNational, testStadiums.ElementAt(i).IsNational);
                Assert.Equal(expectedstadiums.ElementAt(i).Lighting, testStadiums.ElementAt(i).Lighting);
                Assert.Equal(expectedstadiums.ElementAt(i).FieldSize, testStadiums.ElementAt(i).FieldSize);
            }

        }
        [Fact]
        public async Task ReturnStadiumWhichExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetStadiumByIdReturnStadium");

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
                        () => testStadium = await repository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testStadium);
            Assert.Equal(expectedStadium.Id, testStadium.Id);
            Assert.Equal(expectedStadium.Name, testStadium.Name);
            Assert.Equal(expectedStadium.Country, testStadium.Country);
            Assert.Equal(expectedStadium.City, testStadium.City);
            Assert.Equal(expectedStadium.Capacity, testStadium.Capacity);
            Assert.Equal(expectedStadium.VipCapacity, testStadium.VipCapacity);
            Assert.Equal(expectedStadium.Architect, testStadium.Architect);
            Assert.Equal(expectedStadium.BuiltAt, testStadium.BuiltAt);
            Assert.Equal(expectedStadium.IsForDisabled, testStadium.IsForDisabled);
            Assert.Equal(expectedStadium.IsNational, testStadium.IsNational);
            Assert.Equal(expectedStadium.Lighting, testStadium.Lighting);
            Assert.Equal(expectedStadium.FieldSize, testStadium.FieldSize);

        }

        [Fact]
        public async Task ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Arrange
            var repository = SoccerStatisticsContextMocker.GetInMemoryStadiumRepository("GetStadiumByIdReturnNull");

            Stadium testStadium = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await repository.GetByIdAsync(0));

            // Assert
            Assert.Null(err);
            Assert.Null(testStadium);
        }
    }
}
