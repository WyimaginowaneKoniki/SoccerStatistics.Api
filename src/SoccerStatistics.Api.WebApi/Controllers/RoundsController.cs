using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class RoundsController : ApiControllerBase
    {
        private readonly ILogger<RoundsController> _logger;
        public RoundsController(IMediator mediator, ILogger<RoundsController> logger) : base(mediator) 
        { 
            _logger = logger;
        }

        /// <summary>
        ///  Get round by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Rounds/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRoundById([FromRoute] GetRoundByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting round {id}", query.Id);
            var round = await CommandAsync(query);

            if (round == null)
            {
                _logger.LogInformation(LoggingEvents.GetItemNotFound, "Round {id} not found", query.Id);
                return NotFound();
            }

            return Ok(round);
        }

    }
}
