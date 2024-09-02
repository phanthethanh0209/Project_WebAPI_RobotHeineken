namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class UpdateMachineDTO
    {
        public string MachineName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int ContainerStatus { get; set; }
        public int HeinekenCount { get; set; }
        public int OtherCount { get; set; }
        public string City { get; set; }
    }
}
