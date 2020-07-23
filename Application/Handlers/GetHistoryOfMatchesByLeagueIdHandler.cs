using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetHistoryOfMatchesByLeagueIdHandler : IRequestHandler<GetHistoryOfMatchesByLeagueIdQuery, IEnumerable<MatchBasicDTO>>
    {
        private readonly IMatchService _service;

        public GetHistoryOfMatchesByLeagueIdHandler(IMatchService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<MatchBasicDTO>> Handle(GetHistoryOfMatchesByLeagueIdQuery request, CancellationToken cancellationToken)
         => await _service.GetHistoryOfMatchesByLeagueId(request.LeagueId);
    }
}
