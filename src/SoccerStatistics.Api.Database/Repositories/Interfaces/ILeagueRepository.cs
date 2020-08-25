using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ILeagueRepository : IRepository
    {
        Task<IEnumerable<League>> GetAllAsync();
        Task<League> GetByIdAsync(uint id);
    }
}