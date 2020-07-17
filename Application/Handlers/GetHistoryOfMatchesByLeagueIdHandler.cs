using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetHistoryOfMatchesByLeagueIdHandler : IRequestHandler<GetHistoryOfMatchesByLeagueIdQuery, IEnumerable<MatchDTO>>
    {
        private readonly IMatchService _service;

        public GetHistoryOfMatchesByLeagueIdHandler(IMatchService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<MatchDTO>> Handle(GetHistoryOfMatchesByLeagueIdQuery request, CancellationToken cancellationToken)
         => await _service.GetHistoryOfMatchesByLeagueId(request.LeagueId);
    }
}
