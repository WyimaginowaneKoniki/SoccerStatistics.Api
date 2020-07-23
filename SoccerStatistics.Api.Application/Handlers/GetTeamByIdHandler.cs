using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetTeamByIdHandler : IRequestHandler<GetTeamByIdQuery, TeamDTO>
    {
        private readonly ITeamService _service;

        public GetTeamByIdHandler(ITeamService service)
        {
            _service = service;
        }
        public async Task<TeamDTO> Handle(GetTeamByIdQuery query, CancellationToken cancellationToken)
       => await _service.GetByIdAsync(query.Id);

    }
}
