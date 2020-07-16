using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class RoundsController : ApiControllerBase
    {
        public RoundsController(IMediator mediator) : base(mediator) { }

        // GET: api/Rounds/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoundById([FromRoute] GetRoundByIdQuery query)
        {
            var round = await CommandAsync(query);

            if (round == null)
            {
                return NotFound();
            }

            return Ok(round);
        }

    }
}
