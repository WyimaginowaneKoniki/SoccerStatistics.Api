using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface ITeamService : IService
    {
        Task<IEnumerable<TeamBasicDTO>> GetAllAsync();
        Task<TeamDTO> GetByIdAsync(uint id);
    }
}

