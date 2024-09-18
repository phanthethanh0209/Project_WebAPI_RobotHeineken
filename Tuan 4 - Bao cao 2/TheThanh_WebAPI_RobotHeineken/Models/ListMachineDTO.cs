namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class ListMachineDTO
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Status { get; set; }
        public string ContainerStatus { get; set; }
    }
}
