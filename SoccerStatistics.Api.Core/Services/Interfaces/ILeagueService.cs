using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface ILeagueService
    {
        IQueryable<LeagueDTO> GetAll();
        Task<LeagueDTO> GetByIdAsync(uint id);
    }
}