using SoccerStatistics.Api.Database.Entities;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ITeamInLeagueRepository : IRepository
    {
        Task<League> GetLeagueForTeamAsync(uint teamId);
    }
}
