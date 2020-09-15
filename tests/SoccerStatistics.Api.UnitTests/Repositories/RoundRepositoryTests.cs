using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class RoundRepositoryTests : RepositoryTestBase
    {
        private IRoundRepository _roundRepository;

        [Fact]
        public async Task ReturnRoundWhichExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetRoundByIdReturnRound");

            _roundRepository = new RoundRepository(_context);

            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            _context.AddRange(fakeLeague);
            _context.SaveChanges();

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _roundRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testRound.Should().NotBeNull();
            testRound.Should().BeEquivalentTo(fakeLeague[0].Rounds.Where(x => x.Id == 1).FirstOrDefault());
        }

        [Fact]
        public async Task ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetRoundByIdReturnNull");

            _roundRepository = new RoundRepository(_context);

            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            _context.AddRange(fakeLeague);
            _context.SaveChanges();

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _roundRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();

            testRound.Should().BeNull();
        }
    }
}
