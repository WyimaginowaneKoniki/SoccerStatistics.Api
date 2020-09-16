using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class MatchRepository : IMatchRepository
    {
        private readonly SoccerStatisticsDbContext _dbContext;

        public MatchRepository(SoccerStatisticsDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Match> GetByIdAsync(uint id)
            => await _dbContext.Matches.Include(m => m.Round)
                                       .Include(m => m.Stadium)
                                       .Include(m => m.Activities)
                                       .Include(m => m.InteractionsBetweenPlayers)
                                       .Include(m => m.TeamOneStats)
                                       .Include(m => m.TeamTwoStats)
                                       .SingleOrDefaultAsync(match => match.Id == id);
    }
}
