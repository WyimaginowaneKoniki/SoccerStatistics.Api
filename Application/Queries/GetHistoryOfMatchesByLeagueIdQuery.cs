using MediatR;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetHistoryOfMatchesByLeagueIdQuery : IRequest<IEnumerable<MatchDTO>>
    {
        public uint LeagueId { get; set; }
    }
}
