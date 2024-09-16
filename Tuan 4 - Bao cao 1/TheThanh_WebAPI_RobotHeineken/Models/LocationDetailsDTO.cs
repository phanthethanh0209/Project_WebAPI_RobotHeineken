namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class LocationDetailsDTO
    {
        public string LocationName { get; set; }
        public string Area { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public List<LocationMachineDTO>? MachineDTOs { get; set; }
    }
}
