using MediatR;
using SoccerStatistics.Api.Core.DTO;
using System;
using System.Collections.Generic;
using System.Text;

namespace SoccerStatistics.Api.Application.Queries
{
    public class GetTeamByIdQuery : IRequest<TeamDTO>
    {
        public uint Id { get; set; }
    }
    
    
}
