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
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            var context = GetInMemory("GetRoundByIdReturnRound", fakeLeague);

            _roundRepository = new RoundRepository(context);

            var expectedRound = fakeLeague[0].Rounds.First();

            Round testRound = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testRound = await _roundRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testRound.Should().NotBeNull();
            testRound.Should().BeEquivalentTo(expectedRound);
        }

        [Fact]
        public async Task ReturnNullWhenRoundDoNotExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1);

            var context = GetInMemory("GetRoundByIdReturnNull", fakeLeague);

            _roundRepository = new RoundRepository(context);

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
