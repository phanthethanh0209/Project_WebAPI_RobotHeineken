namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class CreateMachineDTO
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Description { get; set; }
        public int Capacity { get; set; }
        public int? LocationID { get; set; }

    }
}
