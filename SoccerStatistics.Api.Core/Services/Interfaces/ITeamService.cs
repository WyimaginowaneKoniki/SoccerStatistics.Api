using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<IEnumerable<TeamInListDTO>> GetAllAsync();
        Task<TeamDTO> GetByIdAsync(uint id);
    }
}

