﻿using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace TheThanh_WebAPI_RobotHeineken.Authorization
{
    public class PermissionHandler : AuthorizationHandler<PermissionRequirement>
    {
        private readonly IUserPermission _permissionService;

        public PermissionHandler(IUserPermission permissionService)
        {
            _permissionService = permissionService;
        }

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionRequirement requirement)
        {
            // Lấy userId từ claim
            Claim? userIdClaim = context.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || !int.TryParse(userIdClaim.Value, out int userId))
            {
                return;
            }

            // Lấy danh sách permissions của user từ database
            List<Data.Permission> userPermissions = await _permissionService.GetPermissionsAsync(userId);

            //Kiểm tra nếu user có quyền cần thiết
            if (userPermissions != null && userPermissions.Any(p => p.Name == requirement.Permission))
            {
                context.Succeed(requirement);
            }
        }
    }
}
