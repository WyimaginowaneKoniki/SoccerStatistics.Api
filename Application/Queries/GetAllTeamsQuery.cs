using MediatR;
using SoccerStatistics.Api.Core.DTO;
using System.Collections;
using System.Collections.Generic;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetAllTeamsQuery : IRequest<IEnumerable<TeamBasicDTO>>
    {
    }
}
