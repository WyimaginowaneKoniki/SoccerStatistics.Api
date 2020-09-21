using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SoccerStatistics.Api.WebApi.Controllers
{
    /// <summary>
    /// Base Api Controller
    /// </summary>
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public abstract class ApiControllerBase : ControllerBase
    {
        private readonly IMediator _mediator;
        /// <summary>
        /// Base constructor with Mediator
        /// </summary>
        /// <param name="mediator"></param>
        protected ApiControllerBase(IMediator mediator)
        {
            _mediator = mediator;
        }
        /// <summary>
        /// Command Task of Mediator
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="command"></param>
        /// <returns></returns>
        protected async Task<TResult> CommandAsync<TResult>(IRequest<TResult> command)
            => await _mediator.Send(command);

    }
}
