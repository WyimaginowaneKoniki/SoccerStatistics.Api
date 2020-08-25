using System.Collections.Generic;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class MatchesController : ApiControllerBase
    {
        private readonly ILogger _logger;
        public MatchesController(IMediator mediator, ILogger<MatchesController> logger) : base(mediator)
        { 
            _logger = logger; 
        }

        /// <summary>
        /// Get list of recent matches
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Matches
        [HttpGet]
        public async Task<IActionResult> GetHistoryOfMatchesByLeagueId([FromRoute] GetHistoryOfMatchesByLeagueIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting latest matches from league {id}", query.LeagueId);
            IEnumerable<MatchBasicDTO> matches = await CommandAsync(query);

            if (matches == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "latest matches from league {id} not found", query.LeagueId);
                return NotFound();
            }

            return Ok(matches);
        }

        /// <summary>
        /// Get match by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Matches/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetMatchById([FromRoute] GetMatchByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting match {id}", query.Id);
            MatchDTO match = await CommandAsync(query);

            if (match == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Match {id} not found", query.Id);
                return NotFound();
            }

            return Ok(match);
        }
    }
}
