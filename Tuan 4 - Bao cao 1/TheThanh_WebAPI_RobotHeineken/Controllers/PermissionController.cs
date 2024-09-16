using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PermissionController : ControllerBase
    {
        private readonly IPermissionService _permissionService;

        public PermissionController(IPermissionService permissionService)
        {
            _permissionService = permissionService;
        }

        [HttpPost]
        public async Task<IActionResult> CreatePermission(PermissionDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _permissionService.CreatePermission(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _permissionService.GetAllPermission());
        }

        [HttpPut("{PermissionID}")]
        public async Task<IActionResult> UpdateRole(int PermissionID, PermissionDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _permissionService.UpdatePermission(PermissionID, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _permissionService.GetAllPermission());
        }

        [HttpDelete("{PermissionID}")]
        public async Task<IActionResult> DeleteRole(int PermissionID)
        {
            (bool Success, string ErrorMessage) result = await _permissionService.DeletePermission(PermissionID);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _permissionService.GetAllPermission());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _permissionService.GetAllPermission());
        }

        [HttpGet("{PermissionID}")]
        public async Task<IActionResult> GetByIdRole(int PermissionID)
        {
            PermissionDTO role = await _permissionService.GetPermission(PermissionID);
            if (role == null) return BadRequest("Not found");

            return Ok(role);
        }
    }
}
