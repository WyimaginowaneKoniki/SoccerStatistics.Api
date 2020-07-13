using SoccerStatistics.Api.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Core.Services.Interfaces
{
    public interface ITeamService
    {
        Task<TeamDTO> GetByIdAsync(uint id);
    }
}
}
