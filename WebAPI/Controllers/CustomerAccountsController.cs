
using Business.Handlers.CustomerAccounts.Commands;
using Business.Handlers.CustomerAccounts.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;

namespace WebAPI.Controllers
{
    /// <summary>
    /// CustomerAccounts If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerAccountsController : BaseApiController
    {
        ///<summary>
        ///List CustomerAccounts
        ///</summary>
        ///<remarks>CustomerAccounts</remarks>
        ///<return>List CustomerAccounts</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<CustomerAccount>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCustomerAccountsList")]
        public async Task<IActionResult> GetCustomerAccountsList()
        {
            var result = await Mediator.Send(new GetCustomerAccountsQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>CustomerAccounts</remarks>
        ///<return>CustomerAccounts List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(CustomerAccount))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCustomerAccountById")]
        public async Task<IActionResult> GetCustomerAccountById(int id)
        {
            var result = await Mediator.Send(new GetCustomerAccountQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add CustomerAccount.
        /// </summary>
        /// <param name="createCustomerAccount"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addCustomerAccount")]
        public async Task<IActionResult> AddCustomerAccount([FromBody] CreateCustomerAccountCommand createCustomerAccount)
        {
            var result = await Mediator.Send(createCustomerAccount);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update CustomerAccount.
        /// </summary>
        /// <param name="updateCustomerAccount"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateCustomerAccount")]
        public async Task<IActionResult> UpdateCustomerAccount([FromBody] UpdateCustomerAccountCommand updateCustomerAccount)
        {
            var result = await Mediator.Send(updateCustomerAccount);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete CustomerAccount.
        /// </summary>
        /// <param name="deleteCustomerAccount"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteCustomerAccount")]
        public async Task<IActionResult> DeleteCustomerAccount([FromBody] DeleteCustomerAccountCommand deleteCustomerAccount)
        {
            var result = await Mediator.Send(deleteCustomerAccount);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
