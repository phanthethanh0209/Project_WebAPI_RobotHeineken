using Microsoft.AspNetCore.Authorization;

namespace TheThanh_WebAPI_RobotHeineken.Authorization
{
    public class PermissionRequirement : IAuthorizationRequirement
    {
        public string Permission { get; set; }

        public PermissionRequirement(string permission)
        {
            Permission = permission;
        }
    }
}
