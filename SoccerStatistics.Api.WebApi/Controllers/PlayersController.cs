using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class PlayersController : ApiControllerBase
    {
        public PlayersController(IMediator mediator) : base(mediator) { }

        // GET: api/Players
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllPlayersQuery request)
        {
            var players = await CommandAsync(request);

            if (players == null)
            {
                return NotFound();
            }

            return Ok(players);
        }

        // GET: api/Players/{id}
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
