using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public TeamRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Team>> GetAllAsync()
            => await _context.Teams.ToListAsync();

        public async Task<Team> GetByIdAsync(uint id)
            => await _context.Teams.SingleOrDefaultAsync(x => x.Id == id);
    }
}
