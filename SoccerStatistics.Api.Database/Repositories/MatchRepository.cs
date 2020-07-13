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
            => await _dbContext.Matches.SingleOrDefaultAsync(match => match.Id == id);
    }
}
