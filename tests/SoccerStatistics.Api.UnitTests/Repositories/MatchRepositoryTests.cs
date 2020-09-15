using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{

    public class MatchRepositoryTests : RepositoryTestBase
    {
        private IMatchRepository _matchRepository;

        [Fact]
        public async void ReturnMatchWhichExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetMatchByIdReturnMatch");

            _matchRepository = new MatchRepository(_context);

            var fakeTeams = _fakeData.GetFakeTeam().Generate(4);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            _context.AddRange(fakeLeague);
            _context.SaveChanges();

            var expectedMatch = fakeLeague[0].Rounds.SelectMany(x => x.Matches)
                                                    .Where(x => x.Id == 1)
                                                    .FirstOrDefault();

            Match testMatch = null;

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await _matchRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testMatch.Should().NotBeNull();
            testMatch.Should().BeEquivalentTo(expectedMatch);
        }

        [Fact]
        public async Task ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetMatchByIdReturnNull");

            _matchRepository = new MatchRepository(_context);

            var fakeTeams = _fakeData.GetFakeTeam().Generate(4);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            _context.AddRange(fakeLeague);
            _context.SaveChanges();

            Match testMatch = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await _matchRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();
            testMatch.Should().BeNull();
        }
    }
}
