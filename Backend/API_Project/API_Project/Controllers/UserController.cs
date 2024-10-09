using API_Project.Entities;
using API_Project.IServices;
using API_Project.RequestModel;
using Microsoft.AspNetCore.Mvc;
using OneOf.Types;

namespace API_Project.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        #region declarations
        private readonly IUserService _userService;
        #endregion

        #region Constructor
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        #endregion

        #region API's
        /// <summary>
        /// Get all users
        /// </summary>
        /// <returns></returns>
        [HttpGet("get_all_users")]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userService.GetUsersAsync();
            return Ok(users);
        }

        /// <summary>
        /// get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("get_user/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _userService.GetUserByIdAsync(id);

            return result.Match<IActionResult>(
                user => Ok(user), // Return 200 OK with user data
                errorMessage => NotFound(errorMessage) // Return 404 Not Found with error message
            );
        }


        /// <summary>
        /// Add new user
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPost("create_user")]
        public async Task<IActionResult> CreateUser([FromBody] UserRequestModel user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.CreateUserAsync(user);

            return result.Match<IActionResult>(
               user => Ok(user), 
               errorMessage => BadRequest(errorMessage)
           );
        }

        /// <summary>
        /// update user details
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        [HttpPut("update_user")]
        public async Task<IActionResult> UpdateUser(User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var result = await _userService.UpdateUserAsync(user);
            return result.Match<IActionResult>(
               user => Ok(user),
               error => BadRequest(error)
           );
        }

        /// <summary>
        /// delete user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete("delete_user/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var result = await _userService.DeleteUserAsync(id);
            if (result == "User deleted successfully")
                return Ok(result);
            else
                return BadRequest(result); 
        }

        #endregion
    }
}
