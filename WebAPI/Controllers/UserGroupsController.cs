using Business.Handlers.UserGroups.Commands;
using Business.Handlers.UserGroups.Queries;
using Core.Entities.Concrete;
using Core.Entities.Dtos;
using Entities.Dtos;
using Microsoft.AspNetCore.Authorization;
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
    [Route("api/v{version:apiVersion}/user-groups")]
    [ApiController]
    public class UserGroupsController : BaseApiController
    {
        /// <summary>
        /// List UserGroup
        /// </summary>
        /// <remarks>bla bla bla UserGroups</remarks>
        /// <return>Kullanıcı Grup List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUserGroupsList")]
        public async Task<IActionResult> GetUserGroupsList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupsQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUserGroupByUserId/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserGroupByUserId([FromRoute] int userId)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupLookupQuery { UserId = userId }));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getGroupClaims/{id}/groups")]
        public async Task<IActionResult> GetGroupClaimsByUserId([FromRoute] int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserGroupLookupByUserIdQuery { UserId = id }));
        }


        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>UserGroups List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserGroup>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUsersInGroupByGroupid/{id}/users")]
        public async Task<IActionResult> GetUsersInGroupByGroupid([FromRoute] int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUsersInGroupLookupByGroupIdQuery
            { GroupId = id }));
        }

        /// <summary>
        /// Add UserGroup.
        /// </summary>
        /// <param name="createUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddUserGroup([FromBody] CreateUserGroupCommand createUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUserGroup));
        }

        /// <summary>
        /// Update UserGroup.
        /// </summary>
        /// <param name="updateUserGroup"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> UpdateUserGroup([FromBody] UpdateUserGroupCommand updateUserGroup)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(updateUserGroup));
        }

        /// <summary>
        /// Update UserGroup by Id.
        /// </summary>
        /// <param name="updateUserGroupByGroupIdDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("UpdateByGroupId")]
        public async Task<IActionResult> UpdateByGroupId([FromBody] UpdateUserGroupByGroupIdDto updateUserGroupByGroupIdDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateUserGroupByGroupIdCommand { GroupId = updateUserGroupByGroupIdDto.GroupId, UserIds = updateUserGroupByGroupIdDto.UserIds }));
        }

        /// <summary>
        /// Delete UserGroup.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("DeleteUserGroup/{id}")]
        public async Task<IActionResult> DeleteUserGroup([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteUserGroupCommand { Id = id }));
        }
    }
}