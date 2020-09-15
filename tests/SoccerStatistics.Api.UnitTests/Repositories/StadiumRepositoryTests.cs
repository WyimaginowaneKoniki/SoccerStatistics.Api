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
            _context = GetInMemory("GetAllStadiums");

            _stadiumRepository = new StadiumRepository(_context);

            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            _context.AddRange(fakeStadiums);
            _context.SaveChanges();

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
            _context = GetInMemory("GetStadiumByIdReturnStadium");

            _stadiumRepository = new StadiumRepository(_context);

            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            _context.AddRange(fakeStadiums);
            _context.SaveChanges();

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
            _context = GetInMemory("GetStadiumByIdReturnNull");

            _stadiumRepository = new StadiumRepository(_context);

            var fakeStadiums = _fakeData.GetFakeStadium().Generate(3);

            _context.AddRange(fakeStadiums);
            _context.SaveChanges();

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
