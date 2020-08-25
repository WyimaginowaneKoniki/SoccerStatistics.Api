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
        private readonly IMapper _mapper;

        public TeamService(ITeamRepository teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TeamBasicDTO>> GetAllAsync()
        {
            var teams = await _teamRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<TeamBasicDTO>>(teams);
        }

        public async Task<TeamDTO> GetByIdAsync(uint id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }
    }
}
