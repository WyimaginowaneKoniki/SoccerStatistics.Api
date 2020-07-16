using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetStadiumByIdQuery : IRequest<StadiumDTO>
    {
        public uint Id { get; set; }
    }
}
