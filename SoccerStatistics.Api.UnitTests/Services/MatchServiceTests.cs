using AutoMapper;
using Moq;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using System;
using System.Collections.Generic;
using Xunit;
using SoccerStatistics.Api.Core.Services;
using System.Linq;
using Match = SoccerStatistics.Api.Database.Entities.Match;
using FluentAssertions;
using SoccerStatistics.Api.Database.Repositories.Interfaces;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class MatchServiceTests
    {
        [Fact]
        public async void ReturnHistoryOfMatchesWhichExistsInDbByGivenLeagueId()
        {
            // Arrange
            var league = new League { Id = 5, Name = "League5", };
            var round = new Round { Id = 1, Name = "Round1", League = league };
            var match1 = new Match { Id = 1, Round = round, Date = new DateTime(2020, 07, 16) };
            var match2 = new Match { Id = 2, Round = round, Date = new DateTime(2020, 06, 15) };
            var match3 = new Match { Id = 3, Round = round, Date = new DateTime(2019, 03, 13) };
            var match4 = new Match { Id = 4, Round = round, Date = new DateTime(2019, 02, 12) };
            var match5 = new Match { Id = 5, Round = round, Date = new DateTime(2019, 04, 14) };
            var match6 = new Match { Id = 6, Round = round, Date = new DateTime(2015, 07, 9)};
            IEnumerable<Match> matches = new List<Match>
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
            IEnumerable<MatchBasicDTO> testMatches = null;
            var matchRepositoryMock = new Mock<IMatchRepository>();
            matchRepositoryMock.Setup(r => r.GetHistoryOfMatchesByLeagueId(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            matchRepositoryMock.Setup(r => r.GetHistoryOfMatchesByLeagueId(5)).ReturnsAsync(matches);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperLeagueProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
                cfg.AddProfile<AutoMapperMatchProfile>();
            });

            var mapper = new Mapper(configuration);

            IEnumerable<MatchBasicDTO> expectedMatches = mapper.Map<IEnumerable<MatchBasicDTO>>(matches);

            var service = new MatchService(matchRepositoryMock.Object, mapper);

            //Act
            var err = await Record.ExceptionAsync(async () => testMatches = await service.GetHistoryOfMatchesByLeagueId(5));

            // Assert
            err.Should().BeNull();

            testMatches.Should().NotBeNull();

            testMatches.Should().BeEquivalentTo(expectedMatches);
        }

        [Fact]
        public async void ReturnMatchWhichExistsInDbByGivenId()
        {
            // Arrange
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

            var match = new Match
            {
                Id = 1,
                Stadium = stadium,
                AmountOfFans = 60_123,
                Date = new DateTime(2015, 3, 4),
                TeamOneStats = new TeamInMatchStats()
                {
                    Id = 1,
                    Team = team1
                },
                TeamTwoStats = new TeamInMatchStats()
                {
                    Id = 2,
                    Team = team2
                },
                Round = new Round()
                {
                    Id = 1,
                    Name = "Round 1"
                },
                Activities = new List<Activity>()
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
                    },
                InteractionsBetweenPlayers = new List<InteractionBetweenPlayers>()
                    {
                        new InteractionBetweenPlayers()
                        {
                            Id = 1,
                            InteractionType = InteractionType.Goal,
                            Player1 = player1,
                            Player2 = player3
                        }
                    }
            };

            MatchDTO testMatch = null;


            var matchRepositoryMock = new Mock<IMatchRepository>();
            matchRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            matchRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(match);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperActivityProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
                cfg.AddProfile<AutoMapperMatchProfile>();
                cfg.AddProfile<AutoMapperInteractionBetweenPlayersProfile>();
            });

            var mapper = new Mapper(configuration);

            MatchDTO expectedMatch = mapper.Map<MatchDTO>(match);
            FillTeamsInMatchStats(match, expectedMatch, mapper);

            var service = new MatchService(matchRepositoryMock.Object, mapper);
            
            //Act
            var err = await Record.ExceptionAsync(async () => testMatch = await service.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testMatch.Should().NotBeNull();

            testMatch.Should().BeEquivalentTo(expectedMatch);
        }

        [Fact]
        public async void ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            MatchDTO testMatch = null;

            var matchRepositoryMock = new Mock<IMatchRepository>();
            matchRepositoryMock.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ReturnsAsync((Database.Entities.Match)null);

            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperActivityProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
                cfg.AddProfile<AutoMapperInteractionBetweenPlayersProfile>();
            });

            var mapper = new Mapper(configuration);

            var service = new MatchService(matchRepositoryMock.Object, mapper);

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await service.GetByIdAsync(125215));
            // Assert
            err.Should().BeNull();

            testMatch.Should().BeNull();
        }

        private void FillTeamsInMatchStats(Match match, MatchDTO matchDTO, IMapper mapper)
        {
            matchDTO.TeamInMatchStats1 = new TeamInMatchStatsDTO();
            matchDTO.TeamInMatchStats2 = new TeamInMatchStatsDTO();
            FillTeamInMatchStats(match, match.TeamOneStats, matchDTO.TeamInMatchStats1, mapper);
            FillTeamInMatchStats(match, match.TeamTwoStats, matchDTO.TeamInMatchStats2, mapper);
        }

        private void FillTeamInMatchStats(Match match, TeamInMatchStats stats, TeamInMatchStatsDTO statsDTO, IMapper mapper)
        {
            statsDTO.Id = stats.Id;

            statsDTO.BallPossesion = stats.BallPossesion;

            statsDTO.Clearances = (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.ShotOnGoal && stats.Team.Players.Contains(x.Player2)).Count();

            statsDTO.CornerKicks = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.CornerKick && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Formation = stats.Formation;
            statsDTO.Fouls = (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Foul && stats.Team.Players.Contains(x.Player1)).Count();

            statsDTO.FreeKicks = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.FreeKick && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Goals = mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Goal && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Injuries = mapper.Map<IEnumerable<ActivityDTO>>(match.Activities
                .Where(x => x.ActivityType == ActivityType.Injury && stats.Team.Players.Contains(x.Player)));

            statsDTO.Offsides = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.Offside && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Pass = stats.Pass;

            statsDTO.PassSuccess = stats.PassSuccess;

            statsDTO.PassSuccessPercentage = stats.PassSuccess != 0 ? (uint)(stats.Pass / stats.PassSuccess) : 0;

            statsDTO.PenaltyKicks = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.PenaltyKick && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Players = mapper.Map<IEnumerable<PlayerBasicDTO>>(match.Activities
                .Where(x => x.ActivityType == ActivityType.Squad && stats.Team.Players.Contains(x.Player)).Select(x => x.Player));

            statsDTO.RedCards = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.RedCard && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Shots = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.MissedShot && stats.Team.Players.Contains(x.Player)).Count()
                + (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.ShotOnGoal && stats.Team.Players.Contains(x.Player1)).Count()
                + (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Goal && stats.Team.Players.Contains(x.Player1)).Count();

            statsDTO.ShotsOnGoal = (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.ShotOnGoal && stats.Team.Players.Contains(x.Player1)).Count()
                + (uint)match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Goal && stats.Team.Players.Contains(x.Player1)).Count();

            statsDTO.ShotsOnGoalPercentage = statsDTO.ShotsOnGoalPercentage != 0 ? (uint)(statsDTO.ShotsOnGoal / statsDTO.ShotsOnGoalPercentage) : 0;

            statsDTO.Substitutions = mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Change && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Team = mapper.Map<TeamBasicDTO>(stats.Team);

            statsDTO.YellowCards = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.YellowCard && stats.Team.Players.Contains(x.Player)).Count();
        }
    }
}
