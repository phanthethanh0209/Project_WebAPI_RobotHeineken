namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class Location
    {
        public int LocationID { get; set; }
        public string LocationName { get; set; }
        public string City { get; set; }
        public string District { get; set; }
        public string Ward { get; set; }
        public decimal Longitude { get; set; }
        public decimal Latitude { get; set; }
        public DateTime CreateAt { get; set; }

        public ICollection<RecyclingMachine> RecyclingMachines { get; set; } = new List<RecyclingMachine>();

    }
}
