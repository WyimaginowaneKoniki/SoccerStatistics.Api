using FluentAssertions;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{

    public class MatchRepositoryTests
    {
        private IMatchRepository _matchRepository;

        [Fact]
        public async void ReturnHistoryOfMatchesWhichExistsInDbByGivenLeagueId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryHistoryOfMatchesRepository("ReturnHistoryOfMatchesWhichExistsInDbByGivenLeagueId");
            var league = new League { Id = 5, Name = "League5" };
            var round = new Round { Id = 1, Name = "Round1" };
            var match1 = new Match { Id = 1, Round = round, Date = new DateTime(2020, 07, 16) };
            var match2 = new Match { Id = 2, Round = round, Date = new DateTime(2020, 07, 15) };
            var match3 = new Match { Id = 3, Round = round, Date = new DateTime(2019, 03, 13) };
            var match4 = new Match { Id = 4, Round = round, Date = new DateTime(2019, 02, 12) };
            var match5 = new Match { Id = 5, Round = round, Date = new DateTime(2019, 04, 14) };
            var match6 = new Match { Id = 6, Round = round, Date = new DateTime(2020, 07, 9) };

            IEnumerable<Match> expectedMatches = new List<Match>
            {
                match1,
                match2,
                match5,
                match3,
                match4
            };
            league.Rounds = new List<Round>
            {
                round
            };
            round.Matches = new List<Match>
            {
                match1,
                match2,
                match3,
                match4,
                match5,
                match6,
            };
            IEnumerable<Match> testMatches = null;

            //Act
            var err = await Record.ExceptionAsync(async () => testMatches = await _matchRepository.GetHistoryOfMatchesByLeagueId(5));

            // Assert
            err.Should().BeNull();

            testMatches.Should().NotBeNull();

            testMatches.Should().BeEquivalentTo(expectedMatches, options => options.Excluding(x => x.Round));
        }

        [Fact]
        public async void ReturnMatchWhichExistsInDbByGivenId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnMatch");

            var expectedMatch = new Match
            {
                Id = 1,
                AmountOfFans = 60_123,
                Date = new DateTime(2015, 3, 4),
            };

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
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnNull");

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
