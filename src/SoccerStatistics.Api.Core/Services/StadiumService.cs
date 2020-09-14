using AutoMapper;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public class StadiumService : IStadiumService
    {
        private readonly IStadiumRepository _stadiumRepository;
        private readonly ITeamRepository _teamRepository;
        private readonly IMapper _mapper;

        public StadiumService(IStadiumRepository stadiumRepository, ITeamRepository teamRepository, IMapper mapper)
        {
            _stadiumRepository = stadiumRepository;
            _teamRepository = teamRepository;
            _mapper = mapper;
        }
        
        public async Task<IEnumerable<StadiumDTO>> GetAllAsync()
        {
            var stadiums = await _stadiumRepository.GetAllAsync();
            var stadiumsDTO = _mapper.Map<IEnumerable<StadiumDTO>>(stadiums);

            foreach (var stadiumDTO in stadiumsDTO)
            {
                var teams = await _teamRepository.GetByStadiumIdAsync(stadiumDTO.Id);
                stadiumDTO.Teams = _mapper.Map<IEnumerable<TeamBasicDTO>>(teams);
            }

            return stadiumsDTO;
        }

        public async Task<StadiumDTO> GetByIdAsync(uint id)
        {
            // index starts from 1
            if (id == 0)
                return null;

            var stadium = await _stadiumRepository.GetByIdAsync(id);
            var stadiumDTO = _mapper.Map<StadiumDTO>(stadium);
            
            var teams = await _teamRepository.GetByStadiumIdAsync(stadiumDTO.Id);
            stadiumDTO.Teams = _mapper.Map<IEnumerable<TeamBasicDTO>>(teams);

            return stadiumDTO;
        }
    }
}
