using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class TeamsController : BaseController
    {
        public TeamsController(IMediator mediator) : base(mediator) { }

        // GET: api/Teams
        [HttpGet]
        public async Task<IActionResult> GetAllTeams([FromRoute] GetAllTeamsQuery query)
        {
            var teams = await CommandAsync(query);

            if (teams == null)
            {
                return NotFound();
            }

            return Ok(teams);
        }

        // GET: api/Team/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTeamById([FromRoute] GetTeamByIdQuery query)
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
