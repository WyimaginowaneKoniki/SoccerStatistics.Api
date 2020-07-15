using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeaguesController : BaseController
    {
        public LeaguesController(IMediator mediator) : base(mediator) { }

        // GET: api/Leagues
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllLeaguesQuery request)
        {
            var leagues = await CommandAsync(request);

            if (leagues == null)
            {
                return NotFound();
            }

            return Ok(leagues);
        }

        // GET: api/Leagues/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById([FromRoute] GetLeagueByIdQuery query)
        {
            var league = await CommandAsync(query);

            if (league == null)
            {
                return NotFound();
            }

            return Ok(league);
        }

    }
}
