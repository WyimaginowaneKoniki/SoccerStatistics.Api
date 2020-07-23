using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetAllTeamsHandler : IRequestHandler<GetAllTeamsQuery, IEnumerable<TeamBasicDTO>>
    {
        private readonly ITeamService _service;

        public GetAllTeamsHandler(ITeamService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<TeamBasicDTO>> Handle(GetAllTeamsQuery request, CancellationToken cancellationToken) 
            => await _service.GetAllAsync();
    }
}
