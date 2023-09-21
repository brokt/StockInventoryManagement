
using Business.Handlers.Categories.Commands;
using Business.Handlers.Categories.Queries;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Entities.Concrete;
using System.Collections.Generic;
using Core.Entities.Dtos;

namespace WebAPI.Controllers
{
    /// <summary>
    /// Categories If controller methods will not be Authorize, [AllowAnonymous] is used.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : BaseApiController
    {
        ///<summary>
        ///List Categories
        ///</summary>
        ///<remarks>Categories</remarks>
        ///<return>List Categories</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Category>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCategoriesList")]
        public async Task<IActionResult> GetCategoriesList()
        {
            var result = await Mediator.Send(new GetCategoriesQuery());
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }
        
        ///<summary>
        ///List Categories
        ///</summary>
        ///<remarks>Categories</remarks>
        ///<return>List Categories</return>
        ///<response code="200"></response>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<HierarchicalDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getHierarchicalCategoriesList")]
        public async Task<IActionResult> GetHierarchicalCategoriesList(int? id)
        {
            var result = await Mediator.Send(new GetHierarchicalCategoriesQuery() { Id = id});
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        ///<summary>
        ///It brings the details according to its id.
        ///</summary>
        ///<remarks>Categories</remarks>
        ///<return>Categories List</return>
        ///<response code="200"></response>  
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Category))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpGet("getCategoryById")]
        public async Task<IActionResult> GetCategoryById(int id)
        {
            var result = await Mediator.Send(new GetCategoryQuery { Id = id });
            if (result.Success)
            {
                return Ok(result.Data);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Add Category.
        /// </summary>
        /// <param name="createCategory"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPost("addCategory")]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryCommand createCategory)
        {
            var result = await Mediator.Send(createCategory);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Update Category.
        /// </summary>
        /// <param name="updateCategory"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpPut("updateCategory")]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryCommand updateCategory)
        {
            var result = await Mediator.Send(updateCategory);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }

        /// <summary>
        /// Delete Category.
        /// </summary>
        /// <param name="deleteCategory"></param>
        /// <returns></returns>
        [Produces("application/json", "text/plain")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [HttpDelete("deleteCategory")]
        public async Task<IActionResult> DeleteCategory([FromBody] DeleteCategoryCommand deleteCategory)
        {
            var result = await Mediator.Send(deleteCategory);
            if (result.Success)
            {
                return Ok(result.Message);
            }
            return BadRequest(result.Message);
        }
    }
}
