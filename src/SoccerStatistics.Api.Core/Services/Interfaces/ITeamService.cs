using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface ITeamService : IService
    {
        Task<IEnumerable<TeamDTO>> GetAllAsync();
        Task<TeamDTO> GetByIdAsync(uint id);
    }
}

