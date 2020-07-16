using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class PlayerController : ApiControllerBase
    {
        public PlayerController(IMediator mediator) : base(mediator) { }

        // GET: api/Player/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPlayerById([FromRoute] GetPlayerByIdQuery query)
        {
            var player = await CommandAsync(query);

            if(player == null)
            {
                return NotFound();
            }

            return Ok(player);
        }

    }
}
