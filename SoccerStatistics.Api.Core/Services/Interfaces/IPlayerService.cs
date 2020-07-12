using SoccerStatistics.Api.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface IPlayerService
    {
        Task<PlayerDTO> GetByIdAsync(uint id);
    }
}
