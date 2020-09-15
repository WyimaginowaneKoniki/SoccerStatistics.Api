using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{
    public class LeagueRepositoryTests : RepositoryTestBase
    {
        private ILeagueRepository _leagueRepository;

        [Fact]
        public async Task ReturnAllLeaguesWhichExistsInDb()
        {
            // Arrange
            _context = GetInMemory("GetAllLeagues");

            _leagueRepository = new LeagueRepository(_context);

            var fakeTeams = _fakeData.GetFakeTeam().Generate(1);
            var expectedLeagues = _fakeData.GetFakeLeague(fakeTeams).Generate(3);

            _context.AddRange(expectedLeagues);
            _context.SaveChanges();

            IEnumerable<League> testLeagues = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeagues = await _leagueRepository.GetAllAsync());

            // Assert          
            err.Should().BeNull();

            testLeagues.Should().NotBeNull();
            testLeagues.Should().HaveSameCount(expectedLeagues);
            testLeagues.Should().BeEquivalentTo(expectedLeagues);
        }

        [Fact]
        public async Task ReturnLeagueWhichExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetLeagueByIdReturnLeague");

            _leagueRepository = new LeagueRepository(_context);

            var fakeTeam = _fakeData.GetFakeTeam().Generate(1);
            var expectedLeague = _fakeData.GetFakeLeague(fakeTeam).Generate(2);

            _context.AddRange(expectedLeague);
            _context.SaveChanges();

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _leagueRepository.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testLeague.Should().NotBeNull();
            testLeague.Should().BeEquivalentTo(expectedLeague[0]);
        }

        [Fact]
        public async Task ReturnNullWhenLeagueDoNotExistsInDbByGivenId()
        {
            // Arrange
            _context = GetInMemory("GetLeagueByIdReturnNull");

            _leagueRepository = new LeagueRepository(_context);

            var fakeTeam = _fakeData.GetFakeTeam().Generate(1);
            var expectedLeague = _fakeData.GetFakeLeague(fakeTeam).Generate(2);

            _context.AddRange(expectedLeague);
            _context.SaveChanges();

            League testLeague = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testLeague = await _leagueRepository.GetByIdAsync(0));

            // Assert
            err.Should().BeNull();
            testLeague.Should().BeNull();
        }
    }
}
