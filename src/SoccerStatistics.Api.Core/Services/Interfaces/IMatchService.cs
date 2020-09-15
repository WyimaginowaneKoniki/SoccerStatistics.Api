using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface IMatchService : IService
    {
        Task<MatchDTO> GetByIdAsync(uint id);
    }
}