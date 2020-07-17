using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        public MatchesController(IMediator mediator) : base(mediator) { }

        // GET: api/Matches
        [HttpGet]
        public async Task<IActionResult> GetHistoryOfMatchesByLeagueId([FromRoute] GetHistoryOfMatchesByLeagueIdQuery query)
        {
            IEnumerable<MatchDTO> matches = await CommandAsync(query);

            if (matches == null)
            {
                return NotFound();
            }

            return Ok(matches);
        }

        // GET: api/Matches/{id}
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
