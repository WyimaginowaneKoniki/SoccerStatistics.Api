using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetAllPlayersHandler : IRequestHandler<GetAllPlayersQuery, IEnumerable<PlayerDTO>>
    {
        private readonly IPlayerService _service;

        public GetAllPlayersHandler(IPlayerService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<PlayerDTO>> Handle(GetAllPlayersQuery request, CancellationToken cancellationToken)
         => await _service.GetAllAsync();
    }
}
