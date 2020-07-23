using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetAllLeaguesHandler : IRequestHandler<GetAllLeaguesQuery , IEnumerable<LeagueDTO>>
    {
        private readonly ILeagueService _service;

        public GetAllLeaguesHandler(ILeagueService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<LeagueDTO>> Handle(GetAllLeaguesQuery request, CancellationToken cancellationToken)
         => await _service.GetAllAsync();
    }
}
