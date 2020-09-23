using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using System.Threading.Tasks;

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
        /// Get match by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Matches/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(MatchDTO), 200)]
        [ProducesResponseType(404)]
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
