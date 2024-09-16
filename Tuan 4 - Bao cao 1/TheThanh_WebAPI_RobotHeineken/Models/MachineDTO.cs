namespace TheThanh_WebAPI_RobotHeineken.Models
{
    public class MachineDTO
    {
        public int MachineCode { get; set; }
        public string MachineName { get; set; }
        public string Status { get; set; }
        public string ContainerStatus { get; set; }
        public int TotalInteractions { get; set; }
        public int TotalOtherCans { get; set; }
        public int TotalHeinekenCans { get; set; }
        public int Capacity { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime? DeploymentDate { get; set; }
        public int? LocationID { get; set; }
    }
}
