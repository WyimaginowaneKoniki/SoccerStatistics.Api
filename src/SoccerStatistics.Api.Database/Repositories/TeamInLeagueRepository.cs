using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class TeamInLeagueRepository : ITeamInLeagueRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public TeamInLeagueRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        // Return the latest league where the team played in
        public async Task<League> GetLeagueForTeamAsync(uint teamId)
        => await _context.Team_in_league.Where(t => t.Team.Id == teamId)
                                        .OrderByDescending(t => t.Id)
                                        .Select(t => t.League)
                                        .FirstOrDefaultAsync();
    }
}
