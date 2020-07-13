using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDTO> GetByIdAsync(uint id);
    }
}

