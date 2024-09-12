﻿using Microsoft.AspNetCore.Mvc;
using TheThanh_WebAPI_RobotHeineken.Models;
using TheThanh_WebAPI_RobotHeineken.Services;

namespace TheThanh_WebAPI_RobotHeineken.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _roleService;

        public RoleController(IRoleService roleService)
        {
            _roleService = roleService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateRole(RoleDTO createDto)
        {
            (bool Success, string ErrorMessage) result = await _roleService.CreateRole(createDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }
            return Ok(await _roleService.GetAllRole());
        }

        [HttpPut("{RoleID}")]
        public async Task<IActionResult> UpdateRole(int roleId, RoleDTO updateDto)
        {
            (bool Success, string ErrorMessage) result = await _roleService.UpdateRole(roleId, updateDto);

            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _roleService.GetAllRole());
        }

        [HttpDelete("{RoleID}")]
        public async Task<IActionResult> DeleteRole(int roleId)
        {
            (bool Success, string ErrorMessage) result = await _roleService.DeleteRole(roleId);
            if (!result.Success)
            {
                return BadRequest(result.ErrorMessage);
            }

            return Ok(await _roleService.GetAllRole());
        }

        [HttpGet]
        public async Task<IActionResult> GetAllRole()
        {
            return Ok(await _roleService.GetAllRole());
        }

        [HttpGet("{RoleID}")]
        public async Task<IActionResult> GetByIdRole(int roleId)
        {
            RoleDTO role = await _roleService.GetRole(roleId);
            if (role == null) return BadRequest("Not found");

            return Ok(role);
        }
    }
}
