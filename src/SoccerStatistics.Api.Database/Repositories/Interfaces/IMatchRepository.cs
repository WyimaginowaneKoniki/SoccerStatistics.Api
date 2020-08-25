using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface IMatchRepository : IRepository
    {
        Task<Match> GetByIdAsync(uint id);
        Task<IEnumerable<Match>> GetHistoryOfMatchesByLeagueId(uint leagueId);
    }
}