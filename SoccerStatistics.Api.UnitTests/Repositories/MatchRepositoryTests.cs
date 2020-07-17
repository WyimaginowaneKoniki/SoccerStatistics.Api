using DeepEqual.Syntax;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using SoccerStatistics.Api.UnitTests.SportStatisticsContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace SoccerStatistics.Api.UnitTests.Repositories
{

    public class MatchRepositoryTests
    {
        private IMatchRepository _matchRepository;

        [Fact]
        public async void ReturnMatchWhicExistsInDbByGivenId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnMatch");

            var stadium = new Stadium
            {
                Id = 1,
                Name = "Old Trafford",
                Country = "England",
                City = "Manchester",
                BuiltAt = 1910,
                Capacity = 75_797,
                FieldSize = "105:68",
                Cost = 151_233M,
                VipCapacity = 4000,
                IsForDisabled = true,
                Lighting = 100_000,
                Architect = "Archibald Leitch",
                IsNational = false
            };

            var player1 = new Player()
            {
                Id = 1,
                Name = "Paul",
                Surname = "Pogba",
            };

            var player2 = new Player()
            {
                Id = 2,
                Name = "Juan",
                Surname = "Mata"
            };

            var player3 = new Player()
            {
                Id = 3,
                Name = "David",
                Surname = "Silva",
            };

            var player4 = new Player()
            {
                Id = 4,
                Name = "Raheem",
                Surname = "Sterling"
            };



            var team1 = new Team()
            {
                Id = 1,
                FullName = "Manchester United FC",
                Players = new List<Player>()
                {
                    player1,
                    player2
                }
            };

            var team2 = new Team()
            {
                Id = 2,
                FullName = "Manchester City FC",
                Players = new List<Player>()
                {
                    player3,
                    player4

                }
            };
            player1.Team = team1;
            player2.Team = team1;
            player3.Team = team2;
            player4.Team = team2;

            var activities = new List<Activity>()
                    {
                        new Activity()
                        {
                            Id = 1,
                            ActivityType = ActivityType.Squad,
                            Player = player1
                        },
                        new Activity()
                        {
                            Id = 2,
                            ActivityType = ActivityType.Squad,
                            Player = player2
                        },
                        new Activity()
                        {
                            Id = 3,
                            ActivityType = ActivityType.Squad,
                            Player = player3
                        },
                        new Activity()
                        {
                            Id = 4,
                            ActivityType = ActivityType.Squad,
                            Player = player4
                        },
                        new Activity()
                        {
                            Id = 5,
                            ActivityType = ActivityType.RedCard,
                            Player = player2
                        },
                        new Activity()
                        {
                            Id = 6,
                            ActivityType = ActivityType.MissedShot,
                            Player = player3
                        }
                    };
            var interactions = new List<InteractionBetweenPlayers>()
                    {
                        new InteractionBetweenPlayers()
                        {
                            Id = 1,
                            InteractionType = InteractionType.Goal,
                            Player1 = player1,
                            Player2 = player3
                        }
                    };



            var expectedMatch = new Match
            {
                Id = 1,
                Stadium = stadium,
                AmountOfFans = 60_123,
                Date = new DateTime(2015, 3, 4),
                Team1 = new TeamInMatchStats()
                {
                    Team = team1
                },
                Team2 = new TeamInMatchStats()
                {
                    Team = team2
                },
                Round = new Round()
                {
                    Id = 1,
                    Name = "Round 1"
                },
                Activities = activities,
                InteractionsBetweenPlayers = interactions
            };

            player1.InteractionsBetweenPlayer1 = new HashSet<InteractionBetweenPlayers>() { interactions.ElementAt(0) };
            player3.InteractionsBetweenPlayer1 = new HashSet<InteractionBetweenPlayers>() { interactions.ElementAt(0) };

            expectedMatch.Round.Matches = new List<Match>() { expectedMatch };
            
            Match testMatch = null;

            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await _matchRepository.GetByIdAsync(1));

            // Assert
            Assert.Null(err);
            Assert.NotNull(testMatch);

            testMatch.WithDeepEqual(expectedMatch).Assert();
        }

        [Fact]
        public async Task ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            _matchRepository = SoccerStatisticsContextMocker.GetInMemoryMatchRepository("GetMatchByIdReturnNull");

            Match testMatch = null;

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await _matchRepository.GetByIdAsync(523829857));

            // Assert
            Assert.Null(err);
            Assert.Null(testMatch);
        }
    }
}
