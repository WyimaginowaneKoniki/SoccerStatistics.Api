using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SoccerStatistics.Api.Application.Queries;
using SoccerStatistics.Api.Core.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SoccerStatistics.Api.WebApi.Controllers
{

    public class PlayersController : ApiControllerBase
    {
        private readonly ILogger _logger;

        public PlayersController(IMediator mediator, ILogger<PlayersController> logger) : base(mediator) 
        { 
            _logger = logger;
        }

        /// <summary>
        /// Get list of available players
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        // GET: api/Players
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<PlayerDTO>), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetAll([FromRoute] GetAllPlayersQuery request)
        {
            _logger.LogInformation(LoggingEvents.ListItems, "Getting all players");
            var players = await CommandAsync(request);

            if (players == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Players not found");
                return NotFound();
            }

            return Ok(players);
        }

        /// <summary>
        ///  Get player by ID
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        // GET: api/Players/{id}
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(PlayerDTO), 200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetPlayerById([FromRoute] GetPlayerByIdQuery query)
        {
            _logger.LogInformation(LoggingEvents.GetItem, "Getting player {id}", query.Id);
            var player = await CommandAsync(query);

            if(player == null)
            {
                _logger.LogWarning(LoggingEvents.GetItemNotFound, "Player {id} not found", query.Id);
                return NotFound();
            }

            return Ok(player);
        }

    }
}
