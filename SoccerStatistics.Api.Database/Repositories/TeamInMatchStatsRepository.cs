using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class TeamInMatchStatsRepository : ITeamInMatchStatsRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public TeamInMatchStatsRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<TeamInMatchStats>> GetAllByMatchIdAsync(uint id)
            => await _context.Teams_in_match_stats.Where(x => x.Match.Id == id).ToListAsync();
    }
}

