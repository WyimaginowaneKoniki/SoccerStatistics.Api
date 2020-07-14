using SoccerStatistics.Api.Database.Entities;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public interface ILeagueRepository
    {
        IQueryable<League> GetAll();
        Task<League> GetByIdAsync(uint id);
    }
}