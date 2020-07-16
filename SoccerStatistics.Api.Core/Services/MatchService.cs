using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Schema;

namespace SoccerStatistics.Api.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _mapper = mapper;
        }

        public async Task<MatchDTO> GetByIdAsync(uint id)
        {
            Match match = await _matchRepository.GetByIdAsync(id);
            var matchDTO = _mapper.Map<MatchDTO>(match);
            FillTeamsInMatchStats(match, matchDTO);
            return matchDTO;
        }

        private void FillTeamsInMatchStats(Match match, MatchDTO matchDTO)
        {
            FillTeamInMatchStats(match, match.Team1, matchDTO.TeamInMatchStats1);
            FillTeamInMatchStats(match, match.Team2, matchDTO.TeamInMatchStats2);
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

            statsDTO.PassSuccessPercentage = (uint)(stats.Pass / stats.PassSuccess);

            statsDTO.PenaltyKicks = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.PenaltyKick && stats.Team.Players.Contains(x.Player)).Count();

            statsDTO.Players = _mapper.Map<IEnumerable<PlayerBasicDTO>>(match.Activities
                .Where(x => x.ActivityType == ActivityType.Squad && stats.Team.Players.Contains(x.Player)));

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

            statsDTO.ShotsOnGoalPercentage = (uint)(statsDTO.ShotsOnGoal / statsDTO.ShotsOnGoalPercentage);

            statsDTO.Substitutions = _mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Change && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Team = _mapper.Map<TeamBasicDTO>(stats.Team);

            statsDTO.YellowCards = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.YellowCard && stats.Team.Players.Contains(x.Player)).Count();
        }
    }
}