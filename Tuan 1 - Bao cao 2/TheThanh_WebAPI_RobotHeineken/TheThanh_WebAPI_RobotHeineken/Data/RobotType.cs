namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class RobotType
    {
        public int TypeID { get; set; }
        public string Name { get; set; }

        public ICollection<Robot> Robots { get; set; }
    }
}
