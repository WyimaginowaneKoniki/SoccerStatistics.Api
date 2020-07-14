using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface ILeagueService
    {
        IAsyncEnumerable<LeagueDTO> GetAllAsync();
        Task<LeagueDTO> GetByIdAsync(uint id);
    }
}