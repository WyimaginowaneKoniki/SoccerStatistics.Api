using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LeagueController : BaseController
    {
        public LeagueController(IMediator mediator) : base(mediator) { }
        // GET: api/League/all
        [HttpGet("all")]
        public async Task<IActionResult> GetAll([FromRoute] GetAllLeaguesQuery request)
        {
            var leagues = await CommandAsync(request);

            if (leagues == null)
            {
                return NotFound();
            }

            return Ok(leagues);
        }

        // GET: api/League/{id}
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
