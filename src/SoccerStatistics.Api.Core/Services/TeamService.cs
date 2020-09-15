using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamRepository _teamRepository;
        private readonly ITeamInLeagueRepository _teamInLeagueRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, 
            ITeamInLeagueRepository teamInLeagueRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _teamInLeagueRepository = teamInLeagueRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamDTO>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            var teamsDTO = _mapper.Map<IEnumerable<TeamDTO>>(teams);

            foreach (var teamDTO in teamsDTO)
            {
                var league = await _teamInLeagueRepository.GetLeagueForTeamAsync(teamDTO.Id);
                teamDTO.League = _mapper.Map<LeagueBasicDTO>(league);
            }

            return teamsDTO;
        }

        public async Task<TeamDTO> GetByIdAsync(uint id)
        {
            // index starts from 1
            if (id == 0)
                return null;

            var team = await _teamRepository.GetByIdAsync(id);
            
            // we cannot do anything with null value :/
            if (team is null)
                return null;

            var teamDTO = _mapper.Map<TeamDTO>(team);

            var league = await _teamInLeagueRepository.GetLeagueForTeamAsync(teamDTO.Id);
            teamDTO.League = _mapper.Map<LeagueBasicDTO>(league);

            return teamDTO;
        }
    }
}
