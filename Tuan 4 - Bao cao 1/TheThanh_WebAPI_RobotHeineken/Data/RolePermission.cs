namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class RolePermission
    {
        public int RoleID { get; set; }
        public Role Roles { get; set; }


        public int PermissionID { get; set; }
        public Permission Permissions { get; set; }


    }
}
