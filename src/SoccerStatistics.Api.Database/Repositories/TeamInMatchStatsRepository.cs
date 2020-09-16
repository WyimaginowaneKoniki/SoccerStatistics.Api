using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
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

        public async Task<TeamInMatchStats> GetByIdAsync(uint Id)
        => await _context.Teams_in_match_stats.Include(t => t.Team)
                                                .ThenInclude(t => t.Players)
                                              .Include(t => t.PlayersInFormation)
                                              .Include(t => t.PlayersOnBench)
                                              .SingleOrDefaultAsync(t => t.Id == Id);
    }
}
