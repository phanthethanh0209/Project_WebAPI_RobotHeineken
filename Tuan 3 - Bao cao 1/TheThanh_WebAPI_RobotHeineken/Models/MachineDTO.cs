namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class MachineDTO
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Status { get; set; }
        public string ContainerStatus { get; set; }
        public int HeinekenCount { get; set; }
        public int OtherCount { get; set; }
        public DateTime CreateAt { get; set; }
        public int? LocationID { get; set; }
    }
}
