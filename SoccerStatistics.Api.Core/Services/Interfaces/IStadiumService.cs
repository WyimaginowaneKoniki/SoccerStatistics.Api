using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Core.Services
{
    public interface IStadiumService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.IEnumerable<StadiumDTO>> GetAllAsync();
        System.Threading.Tasks.Task<StadiumDTO> GetByIdAsync(uint id);
    }
}