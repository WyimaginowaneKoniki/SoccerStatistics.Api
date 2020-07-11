using SoccerStatistics.Api.Database.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories.Interfaces
{
    public interface IPlayerRepository
    {
        Task<Player> GetByIdAsync(uint id);
    }
}
