using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public interface IStadiumRepository
    {
        Task<IEnumerable<Stadium>> GetAllAsync();
        Task<Stadium> GetByIdAsync(uint id);
    }
}