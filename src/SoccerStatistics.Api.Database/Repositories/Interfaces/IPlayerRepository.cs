using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface IPlayerRepository : IRepository
    {
        Task<IEnumerable<Player>> GetAllAsync();
        Task<Player> GetByIdAsync(uint id);
    }
}
