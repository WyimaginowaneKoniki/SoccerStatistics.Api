using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface IStadiumService
    {
       Task<IEnumerable<StadiumDTO>> GetAllAsync();
       Task<StadiumDTO> GetByIdAsync(uint id);
    }
}