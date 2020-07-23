using SoccerStatistics.Api.Database.Entities;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public interface IRoundRepository : IRepository
    {
        Task<Round> GetByIdAsync(uint id);
    }
}