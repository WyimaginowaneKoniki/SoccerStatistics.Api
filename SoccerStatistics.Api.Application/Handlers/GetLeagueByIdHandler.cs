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
    public class GetLeagueByIdHandler : IRequestHandler<GetLeagueByIdQuery, LeagueDTO>
    {
        private readonly ILeagueService _service;

        public GetLeagueByIdHandler(ILeagueService service)
        {
            _service = service;
        }
        public async Task<LeagueDTO> Handle(GetLeagueByIdQuery query, CancellationToken cancellationToken)
        => await _service.GetByIdAsync(query.Id);

    }
}
