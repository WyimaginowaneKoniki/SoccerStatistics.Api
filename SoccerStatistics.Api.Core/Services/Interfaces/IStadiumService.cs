using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface IStadiumService : IService
    {
       Task<IEnumerable<StadiumDTO>> GetAllAsync();
       Task<StadiumDTO> GetByIdAsync(uint id);
    }
}