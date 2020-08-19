using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Repositories;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public class LeagueService : ILeagueService
    {
        private readonly ILeagueRepository _leagueRepository;
        private readonly IMapper _mapper;

        public LeagueService(ILeagueRepository leagueRepository, IMapper mapper)
        {
            _leagueRepository = leagueRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<LeagueDTO>> GetAllAsync()
        {
            var leagues = await _leagueRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<LeagueDTO>>(leagues);
        }
        public async Task<LeagueDTO> GetByIdAsync(uint id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);
            return _mapper.Map<LeagueDTO>(league);
        }
    }
}
