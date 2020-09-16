using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetTeamByIdQuery : IRequest<TeamDTO>
    {
        public uint Id { get; set; }
    }
    
    
}
