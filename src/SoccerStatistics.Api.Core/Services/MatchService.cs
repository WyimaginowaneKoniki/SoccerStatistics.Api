﻿using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public async Task<IEnumerable<MatchBasicDTO>> GetHistoryOfMatchesByLeagueId(uint leagueId)
        {
            var matches = await _matchRepository.GetHistoryOfMatchesByLeagueId(leagueId);
            return _mapper.Map<IEnumerable<MatchBasicDTO>>(matches);
        }

        public async Task<MatchDTO> GetByIdAsync(uint id)
        {
            Match match = await _matchRepository.GetByIdAsync(id);                   
            var matchDTO = _mapper.Map<MatchDTO>(match);

            if (matchDTO != null)          
                FillTeamsInMatchStats(match, matchDTO); // It calculates matchDTO values for statsInMeatch for each team

            return matchDTO;
        }

        // Calculates whole MatchDTO's TeamInMatchStats for both Teams 
        private void FillTeamsInMatchStats(Match match, MatchDTO matchDTO)
        {
            matchDTO.TeamInMatchStats1 = new TeamInMatchStatsDTO();
            matchDTO.TeamInMatchStats2 = new TeamInMatchStatsDTO();
            FillTeamInMatchStats(match, match.TeamOneStats, matchDTO.TeamInMatchStats1);
            FillTeamInMatchStats(match, match.TeamTwoStats, matchDTO.TeamInMatchStats2);
        }

        // Calculates TeamInMatchStats for one team
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

            statsDTO.Players = _mapper.Map<IEnumerable<PlayerBasicDTO>>(match.Activities
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

            statsDTO.Substitutions = _mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers
                .Where(x => x.InteractionType == InteractionType.Change && stats.Team.Players.Contains(x.Player1)));

            statsDTO.Team = _mapper.Map<TeamBasicDTO>(stats.Team);

            statsDTO.YellowCards = (uint)match.Activities
                .Where(x => x.ActivityType == ActivityType.YellowCard && stats.Team.Players.Contains(x.Player)).Count();
        }
    }
}