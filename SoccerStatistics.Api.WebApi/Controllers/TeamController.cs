using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class TeamController : BaseController
    {
        public TeamController(IMediator mediator) : base(mediator) { }

        // GET: api/Player/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById([FromRoute] GetTeamByIdQuery query)
        {
            var team = await CommandAsync(query);

            if (team == null)
            {
                return NotFound();
            }

            return Ok(team);
        }

    }
}
