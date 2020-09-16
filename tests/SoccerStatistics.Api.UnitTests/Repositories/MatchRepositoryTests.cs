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
            var fakeTeams = _fakeData.GetFakeTeam().Generate(4);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            var context = GetInMemory("GetMatchByIdReturnMatch", fakeLeague);

            _matchRepository = new MatchRepository(context);

            var expectedMatch = fakeLeague[0].Rounds.SelectMany(x => x.Matches).First();

            Match testMatch = null;

            //Act
            var err = await Record.ExceptionAsync(async 
                        () => testMatch = await _matchRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testMatch.Should().NotBeNull();
            testMatch.Should().BeEquivalentTo(expectedMatch);
        }

        [Fact]
        public async Task ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(4);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            var context = GetInMemory("GetMatchByIdReturnNull", fakeLeague);

            _matchRepository = new MatchRepository(context);

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
