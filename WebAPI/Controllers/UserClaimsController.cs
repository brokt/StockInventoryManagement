﻿using Business.Handlers.UserClaims.Commands;
using Business.Handlers.UserClaims.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entities.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace WebAPI.Controllers
{
    /// <summary>
    /// If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    ///
    [Route("api/v{version:apiVersion}/user-claims")]
    [ApiController]
    public class UserClaimsController : BaseApiController
    {
        /// <summary>
        /// List UserClaims
        /// </summary>
        /// <remarks>bla bla bla UserClaims</remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUserClaimsList")]
        public async Task<IActionResult> GetUserClaimsList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimsQuery()));
        }

        /// <summary>
        /// Id sine göre detaylarını getirir.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUserClaimByUser/{id}")]
        public async Task<IActionResult> GetUserClaimByUserId([FromRoute] int userid)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimLookupQuery { UserId = userid }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getOperationClaimByUser/{id}")]
        public async Task<IActionResult> GetOperationClaimByUserId([FromRoute] int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserClaimLookupByUserIdQuery { Id = id }));
        }

        /// <summary>
        /// Add GroupClaim.
        /// </summary>
        /// <param name="createUserClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddUserClaim([FromBody] CreateUserClaimCommand createUserClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUserClaim));
        }

        /// <summary>
        /// Update GroupClaim.
        /// </summary>
        /// <param name="updateUserClaimDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> UpdateUserClaim([FromBody] UpdateUserClaimDto updateUserClaimDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateUserClaimCommand { UserId = updateUserClaimDto.UserId, ClaimId = updateUserClaimDto.ClaimIds }));
        }

        /// <summary>
        /// Delete GroupClaim.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserClaim([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteUserClaimCommand { Id = id }));
        }
    }
}