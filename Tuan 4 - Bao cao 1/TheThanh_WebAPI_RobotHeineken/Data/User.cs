namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class User
    {
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public ICollection<RoleUser> RoleUsers { get; set; } = new List<RoleUser>();
        public ICollection<RefreshToken> RefreshTokens { get; set; } = new List<RefreshToken>();
        public ICollection<QRCode> QRCodes { get; set; } = new List<QRCode>();


    }
}
