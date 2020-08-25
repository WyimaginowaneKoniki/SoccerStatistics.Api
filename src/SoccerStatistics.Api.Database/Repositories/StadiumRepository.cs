using Microsoft.EntityFrameworkCore;
using SoccerStatistics.Api.Database.Entities;
using SoccerStatistics.Api.Database.Repositories.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public class StadiumRepository : IStadiumRepository
    {
        private readonly SoccerStatisticsDbContext _context;

        public StadiumRepository(SoccerStatisticsDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Stadium>> GetAllAsync()
           => await _context.Stadiums.ToListAsync();


        public async Task<Stadium> GetByIdAsync(uint id)
            => await _context.Stadiums.SingleOrDefaultAsync(x => x.Id == id);


    }
}
