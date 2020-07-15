using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public class MatchService : IMatchService
    {
        private readonly IMatchRepository _matchRepository;
        private readonly ITeamInMatchStatsRepository _teamInMatchStatsRepository;
        private readonly IMapper _mapper;

        public MatchService(IMatchRepository matchRepository,
            ITeamInMatchStatsRepository teamInMatchStatsRepository, IMapper mapper)
        {
            _matchRepository = matchRepository;
            _teamInMatchStatsRepository = teamInMatchStatsRepository;
            _mapper = mapper;
        }

        public async Task<MatchDTO> GetByIdAsync(uint id)
        {
            Match match = await _matchRepository.GetByIdAsync(id);
            IEnumerable<TeamInMatchStats> teamsStats = await _teamInMatchStatsRepository.GetAllByMatchIdAsync(id);

            if (match is null)
                return null;

            return new MatchDTO()
            {
                Id = match.Id,
                Stadium = _mapper.Map<StadiumDTO>(match.Stadium),
                AmountOfFans = match.AmountOfFans,
                Round = _mapper.Map<RoundDTO>(match.Round),
                Date = match.Date,
                TeamInMatchStats1 = _mapper.Map<TeamInMatchStatsDTO>(teamsStats.ElementAtOrDefault(0)),
                TeamInMatchStats2 = _mapper.Map<TeamInMatchStatsDTO>(teamsStats.ElementAtOrDefault(1)),
                Activities = _mapper.Map<IEnumerable<ActivityDTO>>(match.Activities),
                InteractionsBetweenPlayers = _mapper.Map<IEnumerable<InteractionBetweenPlayersDTO>>(match.InteractionsBetweenPlayers)
            };
        }
    }
}