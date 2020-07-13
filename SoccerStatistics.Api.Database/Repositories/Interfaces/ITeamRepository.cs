using SoccerStatistics.Api.Database.Entities;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ITeamRepository
    {
        Task<Team> GetByIdAsync(uint id);
    }
}
