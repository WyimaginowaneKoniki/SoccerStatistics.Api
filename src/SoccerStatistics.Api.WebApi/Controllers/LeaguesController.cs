using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class LeaguesController : ApiControllerBase
    {
        private readonly ILogger<LeaguesController> _logger;
        public LeaguesController(IMediator mediator, ILogger<LeaguesController> logger) : base(mediator) 
        { 
            _logger = logger;
        }

        /// <summary>
        /// Get list of available leagues
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // GET: api/Leagues
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllLeaguesQuery request)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting all items");
            var leagues = await CommandAsync(request);

            if (leagues == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Items not found");
                return NotFound();
            }
            
            return Ok(leagues);
        }

        /// <summary>
        /// Get league by ID
        /// </summary>
        /// <param name="query">x\</param>
        /// <returns></returns>
        // GET: api/Leagues/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetLeagueById([FromRoute] GetLeagueByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting item {id}", query.Id);
            var league = await CommandAsync(query);

            if (league == null)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, "Item {id} not found", query.Id);
                return NotFound();
            }

            return Ok(league);
        }

    }
}
