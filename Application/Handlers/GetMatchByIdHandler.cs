using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    class GetMatchByIdHandler : IRequestHandler<GetMatchByIdQuery, MatchDTO>
    {
        private readonly MatchService _service;

        public GetMatchByIdHandler(MatchService service)
        {
            _service = service;
        }

        public async Task<MatchDTO> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
            => await _service.GetByIdAsync(request.Id);
    }
}
