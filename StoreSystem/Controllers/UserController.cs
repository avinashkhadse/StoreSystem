using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using StoreSystem.Business;
using StoreSystem.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace StoreSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly UserManager _userManager;

        public UserController(UserManager userManager)
        {
            _userManager = userManager;
        }

        [HttpPost("CreateUser")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> CreateUser(User user, string password)
        {
            try
            {
                var createdUser = await _userManager.CreateUserAsync(user, password);
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = createdUser,
                    Message = "User created successfully",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpGet("GetUserById/{id}")]
        [AllowAnonymous]
        public async Task<IActionResult> GetUserById(int id)
        {
            try
            {
                var user = await _userManager.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = user,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpGet("GetAllUser")]
        [AllowAnonymous]
        public async Task<IActionResult> GetAllUser()
        {
            try
            {
                var users = await _userManager.GetAllUsersAsync();
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = users,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpPut("UpdateUser/{id}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> UpdateUser(int id, User updatedUser, string newPassword = null)
        {
            try
            {
                var user = await _userManager.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                user.Username = updatedUser.Username;
                user.Role = updatedUser.Role;

                // Check if the newPassword is provided and not empty
                if (!string.IsNullOrWhiteSpace(newPassword))
                {
                    // Hash the new password and update the PasswordHash property
                    user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(newPassword);
                }

                var updated = await _userManager.UpdateUserAsync(id, user);

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = updated,
                    Message = "success",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }

        [HttpDelete("DeleteUser/{id}")]
        //[Authorize(Roles = "Admin")]
        [AllowAnonymous]
        public async Task<IActionResult> DeleteUser(int id)
        {
            try
            {
                var user = await _userManager.GetUserByIdAsync(id);
                if (user == null)
                    return NotFound();

                var isDeleted = await _userManager.DeleteUserAsync(id);
                if (!isDeleted)
                    return NotFound();

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = null,
                    Message = "User deleted successfully...!!",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }
        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string username, string password)
        {
            try
            {
                /*var user = await _userManager.LoginAsync(username, password);
                if (user == null)
                {
                    // Login failed, return error response
                    ReturnJson rJson = new ReturnJson()
                    {
                        ResponseCode = "401",
                        Data = null,
                        Message = "Invalid credentials",
                        Status = "false"
                    };
                    return Unauthorized(rJson);
                }*/
                var user = await _userManager.LoginAsync(username, password);
                if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                {
                    // Login failed, return error response
                    ReturnJson rJson = new ReturnJson()
                    {
                        ResponseCode = "401",
                        Data = null,
                        Message = "Invalid credentials",
                        Status = "false"
                    };
                    return Unauthorized(rJson);
                }

                // Login successful, generate JWT token and return it to the user
                string token = _userManager.GenerateJwtToken(user);

                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "200",
                    Data = token,
                    Message = "Login successful",
                    Status = "true"
                };
                return Ok(returnJson);
            }
            catch (Exception ex)
            {
                ReturnJson returnJson = new ReturnJson()
                {
                    ResponseCode = "500",
                    Data = null,
                    Message = "Error: " + ex.Message,
                    Status = "false"
                };
                return StatusCode(500, returnJson);
            }
        }       

    }
}
