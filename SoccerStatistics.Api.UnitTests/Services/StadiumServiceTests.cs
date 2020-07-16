using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class StadiumServiceTests
    {

        [Fact]
        public async void ReturnAllStadiumsWhichExistsInDb()
        {
            IEnumerable<Stadium> expectedStadiums = new List<Stadium>
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


            var repositoryMock = new Mock<IStadiumRepository>();
            repositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(expectedStadiums);
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperStadiumProfile>());

            var mapper = new Mapper(configuration);

            var expectedLeague = mapper.Map<IEnumerable<StadiumDTO>>(expectedStadiums);

            var service = new StadiumService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadiums = await service.GetAllAsync());

            // Arrange
            Assert.Null(err);
            Assert.NotNull(testStadiums);
            Assert.Equal(expectedStadiums.Count(), testStadiums.Count());
            for (int i = 0; i < expectedLeague.Count(); i++)
            {
                Assert.Equal(expectedStadiums.ElementAt(i).Id, testStadiums.ElementAt(i).Id);
                Assert.Equal(expectedStadiums.ElementAt(i).Name, testStadiums.ElementAt(i).Name);
                Assert.Equal(expectedStadiums.ElementAt(i).Country, testStadiums.ElementAt(i).Country);
                Assert.Equal(expectedStadiums.ElementAt(i).City, testStadiums.ElementAt(i).City);
                Assert.Equal(expectedStadiums.ElementAt(i).Capacity, testStadiums.ElementAt(i).Capacity);
                Assert.Equal(expectedStadiums.ElementAt(i).VipCapacity, testStadiums.ElementAt(i).VipCapacity);
                Assert.Equal(expectedStadiums.ElementAt(i).Architect, testStadiums.ElementAt(i).Architect);
                Assert.Equal(expectedStadiums.ElementAt(i).BuiltAt, testStadiums.ElementAt(i).BuiltAt);
                Assert.Equal(expectedStadiums.ElementAt(i).IsForDisabled, testStadiums.ElementAt(i).IsForDisabled);
                Assert.Equal(expectedStadiums.ElementAt(i).IsNational, testStadiums.ElementAt(i).IsNational);
                Assert.Equal(expectedStadiums.ElementAt(i).Lighting, testStadiums.ElementAt(i).Lighting);
                Assert.Equal(expectedStadiums.ElementAt(i).FieldSize, testStadiums.ElementAt(i).FieldSize);
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

            var repositoryMock = new Mock<IStadiumRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            repositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(stadium);
            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperStadiumProfile>());

            var mapper = new Mapper(configuration);

            var expectedStadium = mapper.Map<StadiumDTO>(stadium);

            var service = new StadiumService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await service.GetByIdAsync(1));

            // Arrange
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
        public async void ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Assert
            StadiumDTO testStadium = null;

            var repositoryMock = new Mock<IStadiumRepository>();
            repositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Stadium)null);


            var configuration = new MapperConfiguration(cfg
                => cfg.AddProfile<AutoMapperStadiumProfile>());

            var mapper = new Mapper(configuration);


            var service = new StadiumService(repositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await service.GetByIdAsync(1));

            // Arrange
            Assert.Null(err);
            Assert.Null(testStadium);
        }

    }
}
