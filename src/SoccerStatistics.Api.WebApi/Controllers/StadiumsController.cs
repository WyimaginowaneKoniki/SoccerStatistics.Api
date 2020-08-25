using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{
    public class StadiumsController : ApiControllerBase
    {
        private readonly ILogger<StadiumsController> _logger;
        public StadiumsController(IMediator mediator, ILogger<StadiumsController> logger) : base(mediator)
        {
            _logger = logger;
        }

        /// <summary>
        ///  Get list of available stadiums
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // GET: api/Stadiums
        [HttpGet]
        public async Task<IActionResult> GetAll([FromRoute] GetAllStadiumsQuery request)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting all stadiums");
            var stadiums = await CommandAsync(request);

            if (stadiums == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Stadiums not found");
                return NotFound();
            }

            return Ok(stadiums);
        }

        /// <summary>
        /// Get stadium by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Stadiums/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetStadiumById([FromRoute] GetStadiumByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting stadium {id}", query.Id);
            var stadium = await CommandAsync(query);

            if (stadium == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Stadium {id} not found", query.Id);
                return NotFound();
            }

            return Ok(stadium);
        }

    }
}
