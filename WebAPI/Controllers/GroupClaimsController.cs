﻿using Business.Handlers.GroupClaims.Commands;
using Business.Handlers.GroupClaims.Queries;
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
    ///
    /// </summary>
    ///
    [Route("api/v{version:apiVersion}/group-claims")]
    [ApiController]
    public class GroupClaimsController : BaseApiController
    {
        /// <summary>
        /// GroupClaims list
        /// </summary>
        /// <remarks>GroupClaims</remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        // [AllowAnonymous]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<GroupClaim>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getgroupClaimsList")]
        public async Task<IActionResult> GetGroupClaimsList()
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(GroupClaim))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("GetGroupClaimById/{id}")]
        public async Task<IActionResult> GetGroupClaimById([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new GetGroupClaimQuery { Id = id }));
        }

        /// <summary>
        /// Brings up Claims by Group Id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>GroupClaims List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("groups/{id}")]
        public async Task<IActionResult> GetGroupClaimsByGroupId([FromRoute] int id)
        {
            return GetResponseOnlyResultData(
                await Mediator.Send(new GetGroupClaimsLookupByGroupIdQuery { GroupId = id }));
        }

        /// <summary>
        /// Addded GroupClaim .
        /// </summary>
        /// <param name="createGroupClaim"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddGroupClaim([FromBody] CreateGroupClaimCommand createGroupClaim)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createGroupClaim));
        }

        /// <summary>
        /// Update GroupClaim.
        /// </summary>
        /// <param name="updateGroupClaimDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> UpdateGroupClaim([FromBody] UpdateGroupClaimDto updateGroupClaimDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateGroupClaimCommand { Id = updateGroupClaimDto.Id, GroupId = updateGroupClaimDto.GroupId, ClaimIds = updateGroupClaimDto.ClaimIds }));
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
        public async Task<IActionResult> DeleteGroupClaim([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteGroupClaimCommand { Id = id }));
        }
    }
}