using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    class GetMatchByIdQuery : IRequest<MatchDTO>
    {
        public uint Id { get; set; }
    }
}
