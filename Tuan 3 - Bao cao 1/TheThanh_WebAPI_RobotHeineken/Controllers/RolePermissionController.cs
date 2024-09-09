using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolePermissionController : ControllerBase
    {

        private readonly IRolePermissionService _rolePermissionService;

        public RolePermissionController(IRolePermissionService rolePermissionService)
        {
            _rolePermissionService = rolePermissionService;
        }

        [HttpPost]
        public async Task<IActionResult> AddPerrmissionToRole(RolePermissionDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _rolePermissionService.AddPermissionToRole(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _rolePermissionService.GetAllRolePermission());
        }

        [HttpDelete("{RoleID}/{PermissionID}")]
        public async Task<IActionResult> DeleteRolePerrmission(int roleId, int permissionId)
        {
            (bool Success, string ErrorMessage) result = await _rolePermissionService.DeleteRolePermission(roleId, permissionId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _rolePermissionService.GetAllRolePermission());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _rolePermissionService.GetAllRolePermission());
        }

        [HttpGet("{RoleID}")]
        public async Task<IActionResult> GetPerrmissionInRole(int roleId)
        {
            IEnumerable<RolePermissionDTO> role = await _rolePermissionService.GetPerrmissionInRole(roleId);
            if (role == null) return BadRequest("Not found");

            return Ok(role);
        }
    }
}
