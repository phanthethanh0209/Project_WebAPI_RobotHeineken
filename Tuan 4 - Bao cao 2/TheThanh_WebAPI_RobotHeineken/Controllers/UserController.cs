using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;
using static TheThanh_WebAPI_RobotHeineken.Authorization.CustomAuthorizationAttribute;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [CustomAuthorize("GetAll")]
        public async Task<IActionResult> GetAllUser()
        {
            return Ok(await _userService.GetAllUser());
        }

        [HttpGet("{UserName}")]
        [CustomAuthorize("GetSingle")]
        public async Task<IActionResult> GetByNameUser(string username)
        {
            UserDTO user = await _userService.GetUserAsync(username);
            if (user == null) return BadRequest("Not found");

            return Ok(user);
        }

        [HttpPost]
        [CustomAuthorize("Create")]
        public async Task<IActionResult> CreateUser(CreateUserDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _userService.CreateUser(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _userService.GetAllUser());
        }

        [HttpPut("{UserID}")]
        [CustomAuthorize("Update")]
        public async Task<IActionResult> UpdateUser(int userid, UserDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _userService.UpdateUser(userid, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _userService.GetAllUser());
        }

        [HttpDelete("{UserID}")]
        [CustomAuthorize("Delete")]
        public async Task<IActionResult> DeleteUser(int UserID)
        {
            (bool Success, string ErrorMessage) result = await _userService.DeleteUser(UserID);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _userService.GetAllUser());


        }
    }
}
