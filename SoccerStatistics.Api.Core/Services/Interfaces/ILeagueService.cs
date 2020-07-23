using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface ILeagueService : IService
    {
        Task<IEnumerable<LeagueDTO>> GetAllAsync();
        Task<LeagueDTO> GetByIdAsync(uint id);
    }
}