
using Business.Handlers.StockMovements.Commands;
using Business.Handlers.StockMovements.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// StockMovements If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class StockMovementsController : BaseApiController
    {
        ///<summary>
        ///List StockMovements
        ///</summary>
        ///<remarks>StockMovements</remarks>
        ///<return>List StockMovements</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<StockMovement>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getStockMovementsList")]
        public async Task<IActionResult> GetStockMovementsList()
        {
            var result = await Mediator.Send(new GetStockMovementsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>StockMovements</remarks>
        ///<return>StockMovements List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(StockMovement))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getStockMovementById")]
        public async Task<IActionResult> GetStockMovementById(int id)
        {
            var result = await Mediator.Send(new GetStockMovementQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add StockMovement.
        /// </summary>
        /// <param name="createStockMovement"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddStockMovement([FromBody] CreateStockMovementCommand createStockMovement)
        {
            var result = await Mediator.Send(createStockMovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update StockMovement.
        /// </summary>
        /// <param name="updateStockMovement"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> UpdateStockMovement([FromBody] UpdateStockMovementCommand updateStockMovement)
        {
            var result = await Mediator.Send(updateStockMovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete StockMovement.
        /// </summary>
        /// <param name="deleteStockMovement"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete]
        public async Task<IActionResult> DeleteStockMovement([FromBody] DeleteStockMovementCommand deleteStockMovement)
        {
            var result = await Mediator.Send(deleteStockMovement);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
