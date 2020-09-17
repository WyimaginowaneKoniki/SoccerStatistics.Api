using AutoMapper;
using FluentAssertions;
using Moq;
using SoccerStatistics.Api.Core.AutoMapper.Profiles;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;
using Match = SoccerStatistics.Api.Database.Entities.Match;

namespace SoccerStatistics.Api.UnitTests.Services
{
    public class MatchServiceTests
    {
        private readonly Mock<IMatchRepository> _matchRepository;
        private readonly Mock<ITeamInMatchStatsRepository> _teamInMatchStatsRepository;
        private readonly IMapper _mapper;
        private readonly IMatchService _service;
        private readonly IFakeData _fakeData;

        public MatchServiceTests()
        {
            var configuration = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<AutoMapperPlayerProfile>();
                cfg.AddProfile<AutoMapperTeamProfile>();
                cfg.AddProfile<AutoMapperStadiumProfile>();
                cfg.AddProfile<AutoMapperActivityProfile>();
                cfg.AddProfile<AutoMapperRoundProfile>();
                cfg.AddProfile<AutoMapperMatchProfile>();
                cfg.AddProfile<AutoMapperInteractionBetweenPlayersProfile>();
                cfg.AddProfile<AutoMapperFormationProfile>();
                cfg.AddProfile<AutoMapperExtraTimeProfile>();
                cfg.AddProfile<AutoMapperOvertimeProfile>();
                cfg.AddProfile<AutoMapperPenaltyKickProfile>();
            });

            _mapper = new Mapper(configuration);
            _matchRepository = new Mock<IMatchRepository>();
            _teamInMatchStatsRepository = new Mock<ITeamInMatchStatsRepository>();
            _service = new MatchService(_matchRepository.Object, _teamInMatchStatsRepository.Object, _mapper);

            _fakeData = new FakeData();
        }

        [Fact]
        public async void ReturnMatchWhichExistsInDbByGivenId()
        {
            // Arrange
            var fakeTeams = _fakeData.GetFakeTeam().Generate(2);
            var fakeLeague = _fakeData.GetFakeLeague(fakeTeams).Generate(1).First();
            var fakeMatch = fakeLeague.Rounds.First().Matches.First();

            MatchDTO testMatch = null;

            _matchRepository.Reset();
            _matchRepository.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _matchRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeMatch);

            _teamInMatchStatsRepository.Reset();
            _teamInMatchStatsRepository.Setup(r => r.GetByIdAsync(It.IsAny<uint>())).ThrowsAsync(new ArgumentException());
            _teamInMatchStatsRepository.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(fakeMatch.TeamOneStats);
            _teamInMatchStatsRepository.Setup(r => r.GetByIdAsync(2)).ReturnsAsync(fakeMatch.TeamTwoStats);

            MatchDTO expectedMatch = _mapper.Map<MatchDTO>(fakeMatch);
            FillTeamsInMatchStats(fakeMatch, expectedMatch);
                        
            //Act
            var err = await Record.ExceptionAsync(async 
                        () => testMatch = await _service.GetByIdAsync(1));

            // Assert
            err.Should().BeNull();

            testMatch.Should().NotBeNull();
            testMatch.Should().BeEquivalentTo(expectedMatch);

            _teamInMatchStatsRepository.Verify(r => r.GetByIdAsync(It.IsInRange<uint>(1u, 2u, Moq.Range.Inclusive)), Times.Exactly(2));
        }

        [Fact]
        public async void ReturnNullWhenMatchDoNotExistsInDbByGivenId()
        {
            // Arrange
            MatchDTO testMatch = null;

            _matchRepository.Reset();
            _teamInMatchStatsRepository.Reset();

            // Act
            var err = await Record.ExceptionAsync(async
                        () => testMatch = await _service.GetByIdAsync(1));
            // Assert
            err.Should().BeNull();

            testMatch.Should().BeNull();
        }

        private void FillTeamsInMatchStats(Match match, MatchDTO matchDTO)
        {
            matchDTO.TeamInMatchStats1 = new TeamInMatchStatsDTO();
            matchDTO.TeamInMatchStats2 = new TeamInMatchStatsDTO();
            FillTeamInMatchStats(match, match.TeamOneStats, matchDTO.TeamInMatchStats1);
            FillTeamInMatchStats(match, match.TeamTwoStats, matchDTO.TeamInMatchStats2);
        }

        private void FillTeamInMatchStats(Match match, TeamInMatchStats stats, TeamInMatchStatsDTO statsDTO)
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

            statsDTO.Goals = _mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Goal && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Injuries = _mapper.Map<IEnumerable<ActivityDTO>>(match.Activities
                .Where(x => x.ActivityType == ActivityType.Injury && stats.Team.Players.Contains(x.Player)));

            statsDTO.Offsides = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.Offside && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Pass = stats.Pass;

            statsDTO.PassSuccess = stats.PassSuccess;

            statsDTO.PassSuccessPercentage = stats.PassSuccess != 0 ? (uint)(stats.Pass / stats.PassSuccess) : 0;

            statsDTO.PenaltyKicks = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.PenaltyKick && stats.Team.Players.Contains(x.Player)).Count();

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

            statsDTO.Substitutions = _mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Substitution && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Team = _mapper.Map<TeamBasicDTO>(stats.Team);

            statsDTO.YellowCards = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.YellowCard && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.PlayersOnBench = _mapper.Map<IEnumerable<PlayerBasicDTO>>(stats.PlayersOnBench.Select(x => x.Player));
            statsDTO.PlayersInFormation = _mapper.Map<IEnumerable<FormationDTO>>(stats.PlayersInFormation);
        }
    }
}
