using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetLeagueByIdQuery : IRequest<LeagueDTO>
    {
        public uint Id { get; set; }
    }
}
