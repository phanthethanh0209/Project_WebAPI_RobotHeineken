namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class UpdateMachineDTO
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int ContainerStatus { get; set; }
        public int TotalInteractions { get; set; }
        public int TotalCan { get; set; }
        public int Capacity { get; set; }
        public int? LocationID { get; set; }
    }
}
