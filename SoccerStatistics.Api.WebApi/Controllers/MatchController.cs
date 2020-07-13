using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerStatistics.Api.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MatchController : BaseController
    {
        public MatchController(IMediator mediator) : base(mediator) { }

        // GET: api/Match/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchById([FromRoute] GetMatchByIdQuery query)
        {
            MatchDTO match = await CommandAsync(query);

            if (match == null)
            {
                return NotFound();
            }

            return Ok(match);
        }
    }
}
