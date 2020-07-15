using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ITeamInMatchStatsRepository
    {
        Task<IEnumerable<TeamInMatchStats>> GetAllByMatchIdAsync(uint id);
    }
}
