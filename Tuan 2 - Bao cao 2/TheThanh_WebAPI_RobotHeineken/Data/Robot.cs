namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class Robot
    {
        public int RobotID { get; set; }
        public string RobotName { get; set; }
        public int Status { get; set; }
        public DateTime LastAccess { get; set; }
        public int BatteryLevel { get; set; }
        public DateTime CreateAt { get; set; }
        public int? LocationID { get; set; }

        // relationship
        public RobotType Type { get; set; }
        public Location Location { get; set; }
    }
}
