using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class StadiumRepositoryTests : RepositoryTestBase
    {
        private IStadiumRepository _stadiumRepository;

        [Fact]
        public async Task ReturnAllStadiumsWhichExistsInDb()
        {
            // Arrange
            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            var context = GetInMemory("GetAllStadiums", fakeStadiums);

            _stadiumRepository = new StadiumRepository(context);

            IEnumerable<Stadium> testStadiums = null;

            // Act

            var err = await Record.ExceptionAsync(async
                        () => testStadiums = await _stadiumRepository.GetAllAsync());

            // Assert
            err.Should().BeNull();

            testStadiums.Should().NotBeNull();
            testStadiums.Should().HaveSameCount(fakeStadiums);
            testStadiums.Should().BeEquivalentTo(fakeStadiums);
        }

        [Fact]
        public async Task ReturnStadiumWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            var context = GetInMemory("GetStadiumByIdReturnStadium", fakeStadiums);

            _stadiumRepository = new StadiumRepository(context);

            Stadium testStadium = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _stadiumRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testStadium.Should().NotBeNull();
            testStadium.Should().BeEquivalentTo(fakeStadiums[0]);
        }

        [Fact]
        public async Task ReturnNullWhenStadiumDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            var context = GetInMemory("GetStadiumByIdReturnNull", fakeStadiums);

            _stadiumRepository = new StadiumRepository(context);

            Stadium testStadium = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testStadium = await _stadiumRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();

            testStadium.Should().BeNull();
        }
    }
}
