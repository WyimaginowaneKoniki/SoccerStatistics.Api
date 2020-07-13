using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetPlayerByIdHandler : IRequestHandler<GetPlayerByIdQuery, PlayerDTO>
    {
        private readonly IPlayerService _service;

        public GetPlayerByIdHandler(IPlayerService service)
        {
            _service = service;
        }

        public async Task<PlayerDTO> Handle(GetPlayerByIdQuery query, CancellationToken cancellationToken)
            => await _service.GetByIdAsync(query.Id);
    }
}
