using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{ 
    public interface IMatchService : IService
    {
        Task<MatchDTO> GetByIdAsync(uint id);
        Task<IEnumerable<MatchBasicDTO>> GetHistoryOfMatchesByLeagueId(uint leagueId);
    }
}