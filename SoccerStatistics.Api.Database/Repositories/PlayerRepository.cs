using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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

        public async Task<Player> GetByIdAsync(uint id)
            => await _context.Players.SingleOrDefaultAsync(x => x.Id == id);
    }
}
