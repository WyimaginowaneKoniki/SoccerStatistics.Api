using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetRoundByIdHandler : IRequestHandler<GetRoundByIdQuery, RoundDTO>
    {
        private readonly IRoundService _service;

        public GetRoundByIdHandler(IRoundService service)
        {
            _service = service;
        }
        public async Task<RoundDTO> Handle(GetRoundByIdQuery query, CancellationToken cancellationToken)
       => await _service.GetByIdAsync(query.Id);

    }
}
