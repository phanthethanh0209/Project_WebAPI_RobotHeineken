using Microsoft.EntityFrameworkCore;
using TheThanh_WebAPI_RobotHeineken.Data;

namespace TheThanh_WebAPI_RobotHeineken.Authorization
{
    public interface IUserPermission
    {
        Task<List<Permission>> GetPermissionsAsync(int userId);
    }
    public class UserPermission : IUserPermission
    {
        private readonly MyDBContext _db;

        public UserPermission(MyDBContext db)
        {
            _db = db;
        }

        public async Task<List<Permission>> GetPermissionsAsync(int userId)
        {
            // lấy các role của user từ bảng RoleUser
            List<RoleUser> userRoles = await _db.RoleUsers.
                Where(u => u.UserID.Equals(userId)).
                Include(r => r.Roles).ToListAsync();

            List<int> roleId = userRoles.Select(u => u.Roles.RoleID).ToList();

            // lấy các permission của role từ bảng RolePermissions
            List<Permission> permissions = await _db.RolePermissions
                .Where(rp => roleId.Contains(rp.RoleID))
                .Include(rp => rp.Permissions)
                .Select(rp => rp.Permissions)
                .ToListAsync();

            return permissions;
        }
    }
}
