
using Business.Handlers.Customers.Commands;
using Business.Handlers.Customers.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Customers If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomersController : BaseApiController
    {
        ///<summary>
        ///List Customers
        ///</summary>
        ///<remarks>Customers</remarks>
        ///<return>List Customers</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Customer>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCustomersList")]
        public async Task<IActionResult> GetCustomersList()
        {
            var result = await Mediator.Send(new GetCustomersQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Customers</remarks>
        ///<return>Customers List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Customer))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCustomerById")]
        public async Task<IActionResult> GetCustomerById(int id)
        {
            var result = await Mediator.Send(new GetCustomerQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add Customer.
        /// </summary>
        /// <param name="createCustomer"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addCustomer")]
        public async Task<IActionResult> AddCustomer([FromBody] CreateCustomerCommand createCustomer)
        {
            var result = await Mediator.Send(createCustomer);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update Customer.
        /// </summary>
        /// <param name="updateCustomer"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateCustomer")]
        public async Task<IActionResult> UpdateCustomer([FromBody] UpdateCustomerCommand updateCustomer)
        {
            var result = await Mediator.Send(updateCustomer);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete Customer.
        /// </summary>
        /// <param name="deleteCustomer"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteCustomer")]
        public async Task<IActionResult> DeleteCustomer([FromBody] DeleteCustomerCommand deleteCustomer)
        {
            var result = await Mediator.Send(deleteCustomer);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
