namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class LocationDTO
    {
        public string LocationName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
    }
}
