using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
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
        public IQueryable<League> GetAll()
            => _context.Leagues;

        public async Task<League> GetByIdAsync(uint id)
            => await _context.Leagues.SingleOrDefaultAsync(x => x.Id == id);
    }
}
