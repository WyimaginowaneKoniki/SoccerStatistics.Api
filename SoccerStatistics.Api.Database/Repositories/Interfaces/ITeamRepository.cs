using SoccerStatistics.Api.Database.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface ITeamRepository : IRepository
    {
        Task<IEnumerable<Team>> GetAllAsync();
        Task<Team> GetByIdAsync(uint id);
    }
}
