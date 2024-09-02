namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class RecyclingMachine
    {
        public int MachineID { get; set; }
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int ContainerStatus { get; set; }
        public int TotalInteractions { get; set; }
        public int HeinekenCount { get; set; }
        public int OtherCount { get; set; }
        public DateTime CreateAt { get; set; }
        public int? LocationID { get; set; }

        // relationship
        public Location Location { get; set; }

    }
}
