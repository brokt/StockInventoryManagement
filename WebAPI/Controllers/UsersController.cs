﻿using Business.Handlers.Users.Commands;
using Business.Handlers.Users.Queries;
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
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    public class UsersController : BaseApiController
    {
        /// <summary>
        /// List Users
        /// </summary>
        /// <remarks>bla bla bla Users</remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<UserDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getUsersList")]
        public async Task<IActionResult> GetUsersList()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUsersQuery()));
        }

        /// <summary>
        /// User Lookup
        /// </summary>
        /// <remarks>bla bla bla Users</remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<SelectionItem>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("userLookups")]
        public async Task<IActionResult> GetUserLookup()
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserLookupQuery()));
        }

        /// <summary>
        /// It brings the details according to its id.
        /// </summary>
        /// <remarks>bla bla bla </remarks>
        /// <return>Users List</return>
        /// <response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(UserDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            return GetResponseOnlyResultData(await Mediator.Send(new GetUserQuery { UserId = id }));
        }

        /// <summary>
        /// Add User.
        /// </summary>
        /// <param name="createUser"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] CreateUserCommand createUser)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(createUser));
        }

        /// <summary>
        /// Update User.
        /// </summary>
        /// <param name="updateUserDto"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserDto updateUserDto)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new UpdateUserCommand { UserId = updateUserDto.UserId, Email = updateUserDto.Email, FullName = updateUserDto.FullName, MobilePhones = updateUserDto.MobilePhones, Address = updateUserDto.Address, Notes = updateUserDto.Notes }));
        }

        /// <summary>
        /// Delete User.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Consumes("application/json")]
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            return GetResponseOnlyResultMessage(await Mediator.Send(new DeleteUserCommand { UserId = id }));
        }
    }
}