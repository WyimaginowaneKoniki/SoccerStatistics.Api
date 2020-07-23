using MediatR;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using SoccerStatistics.Api.Core.Services.Interfaces;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.Application.Handlers
{
    public class GetAllStadiumsHandler : IRequestHandler<GetAllStadiumsQuery, IEnumerable<StadiumDTO>>
    {
        private readonly IStadiumService _service;
        public GetAllStadiumsHandler(IStadiumService service)
        {
            _service = service;
        }

        public async Task<IEnumerable<StadiumDTO>> Handle(GetAllStadiumsQuery request, CancellationToken cancellationToken)
         => await _service.GetAllAsync();
    }
}
