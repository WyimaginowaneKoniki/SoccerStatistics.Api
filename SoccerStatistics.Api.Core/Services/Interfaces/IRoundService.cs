using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface IRoundService
    {
        Task<RoundDTO> GetByIdAsync(uint id);
    }
}