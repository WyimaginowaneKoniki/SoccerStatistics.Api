using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{

    public class LeagueRepository : ILeagueRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public LeagueRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<League>> GetAllAsync()
           => await _context.Leagues.ToListAsync();


        public async Task<League> GetByIdAsync(uint id)
            => await _context.Leagues.SingleOrDefaultAsync(x => x.Id == id);


    }
}
