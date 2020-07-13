using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    interface IMatchService
    {
        Task<MatchDTO> GetByIdAsync(uint id);
    }
}