using SoccerStatistics.Api.Database.Entities;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ITeamInMatchStatsRepository : IRepository
    {
        Task<TeamInMatchStats> GetByIdAsync(uint Id);
    }
}
