using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface ILeagueService
    {
        Task<IEnumerable<LeagueDTO>> GetAllAsync();
        Task<LeagueDTO> GetByIdAsync(uint id);
    }
}