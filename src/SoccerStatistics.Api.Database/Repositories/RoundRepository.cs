using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class RoundRepository : IRoundRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public RoundRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<Round> GetByIdAsync(uint id)
            => await _context.Rounds.SingleOrDefaultAsync(x => x.Id == id);
    }
}
