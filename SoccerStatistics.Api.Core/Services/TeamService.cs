using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public class TeamService : ITeamService
    {
        private readonly ITeamService _teamRepository;
        private readonly IMapper _mapper;

        public TeamService(ITeamService teamRepository, IMapper mapper)
        {
            _teamRepository = teamRepository;
            _mapper = mapper;
        }

        public async Task<TeamDTO> GetByIdAsync(uint id)
        {
            var team = await _teamRepository.GetByIdAsync(id);
            return _mapper.Map<TeamDTO>(team);
        }
    }
}
