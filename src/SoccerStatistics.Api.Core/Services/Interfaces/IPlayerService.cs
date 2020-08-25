using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface IPlayerService : IService
    {
        Task<IEnumerable<PlayerDTO>> GetAllAsync();
        Task<PlayerDTO> GetByIdAsync(uint id);
    }
}
