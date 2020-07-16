using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StadiumsController : BaseController
    {
        public StadiumsController(IMediator mediator) : base(mediator) { }

        // GET: api/Stadiums
        public async Task<IActionResult> GetAll([FromRoute] GetAllStadiumsQuery request)
        {
            var stadiums = await CommandAsync(request);

            if (stadiums == null)
            {
                return NotFound();
            }

            return Ok(stadiums);
        }

        // GET: api/Stadiums/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStadiumById([FromRoute] GetStadiumByIdQuery query)
        {
            var stadium = await CommandAsync(query);

            if (stadium == null)
            {
                return NotFound();
            }

            return Ok(stadium);
        }

    }
}
