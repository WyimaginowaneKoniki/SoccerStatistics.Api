using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetRoundByIdQuery : IRequest<RoundDTO>
    {
        public uint Id { get; set; }
    }
}
