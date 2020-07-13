using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetMatchByIdQuery : IRequest<MatchDTO>
    {
        public uint Id { get; set; }
    }
}
