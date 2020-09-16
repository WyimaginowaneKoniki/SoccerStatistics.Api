using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class PlayerRepository : IPlayerRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public PlayerRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Player>> GetAllAsync()
        => await _context.Players.Include(p => p.Team)
                                 .ToListAsync();

        public async Task<Player> GetByIdAsync(uint id)
        => await _context.Players.Include(p => p.Team)
                                 .SingleOrDefaultAsync(x => x.Id == id);
    }
}
