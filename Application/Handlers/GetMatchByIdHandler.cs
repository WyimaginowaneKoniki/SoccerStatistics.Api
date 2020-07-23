using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetMatchByIdHandler : IRequestHandler<GetMatchByIdQuery, MatchDTO>
    {
        private readonly IMatchService _service;

        public GetMatchByIdHandler(IMatchService service)
        {
            _service = service;
        }

        public async Task<MatchDTO> Handle(GetMatchByIdQuery request, CancellationToken cancellationToken)
            => await _service.GetByIdAsync(request.Id);
    }
}
