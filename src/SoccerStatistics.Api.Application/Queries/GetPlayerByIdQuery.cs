using MediatR;
using SoccerStatistics.Api.Core.DTO;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetPlayerByIdQuery : IRequest<PlayerDTO>
    {
        public uint Id { get; set; }
    }
}
