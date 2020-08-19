using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetStadiumByIdHandler : IRequestHandler<GetStadiumByIdQuery, StadiumDTO>
    {
        private readonly IStadiumService _service;

        public GetStadiumByIdHandler(IStadiumService service)
        {
            _service = service;
        }
        public async Task<StadiumDTO> Handle(GetStadiumByIdQuery query, CancellationToken cancellationToken)
       => await _service.GetByIdAsync(query.Id);

    }
}
