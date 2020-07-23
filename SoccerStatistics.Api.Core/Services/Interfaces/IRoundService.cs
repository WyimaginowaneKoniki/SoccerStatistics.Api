using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services
{
    public interface IRoundService : IService
    {
        Task<RoundDTO> GetByIdAsync(uint id);
    }
}