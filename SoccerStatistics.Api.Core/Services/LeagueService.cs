using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        public IAsyncEnumerable<LeagueDTO> GetAllAsync()
        {
            var leagues = _leagueRepository.GetAllAsync();
            return _mapper.Map<IAsyncEnumerable<LeagueDTO>>(leagues);
        }
        public async Task<LeagueDTO> GetByIdAsync(uint id)
        {
            var league = await _leagueRepository.GetByIdAsync(id);
            return _mapper.Map<LeagueDTO>(league);
        }
    }
}
