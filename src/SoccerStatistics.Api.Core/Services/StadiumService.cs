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
        private readonly IMapper _mapper;

        public StadiumService(IStadiumRepository stadiumRepository, IMapper mapper)
        {
            _stadiumRepository = stadiumRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<StadiumDTO>> GetAllAsync()
        {
            var stadiums = await _stadiumRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<StadiumDTO>>(stadiums);
        }
        public async Task<StadiumDTO> GetByIdAsync(uint id)
        {
            var stadiums = await _stadiumRepository.GetByIdAsync(id);
            return _mapper.Map<StadiumDTO>(stadiums);
        }
    }
}
