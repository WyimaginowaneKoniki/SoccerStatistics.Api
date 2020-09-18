using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class TeamsController : ApiControllerBase
    {
        private readonly ILogger<TeamsController> _logger;
        public TeamsController(IMediator mediator, ILogger<TeamsController> logger) : base(mediator) 
        { 
            _logger = logger; 
        }

        /// <summary>
        ///  Get list of available teams
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Teams
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<TeamDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAllTeams([FromRoute] GetAllTeamsQuery query)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting all teams");
            var teams = await CommandAsync(query);

            if (teams == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Teams not found");
                return NotFound();
            }

            return Ok(teams);
        }

        /// <summary>
        ///  Get team by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Team/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(TeamDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetTeamById([FromRoute] GetTeamByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting team {id}", query.Id);
            var team = await CommandAsync(query);

            if (team == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Team {id} not found", query.Id);
                return NotFound();
            }

            return Ok(team);
        }
    }
}
