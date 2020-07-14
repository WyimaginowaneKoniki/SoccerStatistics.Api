﻿using SoccerStatistics.Api.Database.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Database.Repositories
{
    public interface ILeagueRepository
    {
        IAsyncEnumerable<League> GetAllAsync();
        Task<League> GetByIdAsync(uint id);
    }
}