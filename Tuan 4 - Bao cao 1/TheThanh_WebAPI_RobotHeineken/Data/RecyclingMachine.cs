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
        public int TotalHeinekenCans { get; set; }
        public int TotalOtherCans { get; set; }
        public int Capacity { get; set; }
        public DateTime? DeploymentDate { get; set; }
        public DateTime CreateAt { get; set; }

        // relationship
        public int? LocationID { get; set; }
        public Location Location { get; set; }
        public ICollection<RecyclingHistory> RecyclingHistories { get; set; } = new List<RecyclingHistory>();
        public ICollection<ContainerFullHistory> ContainerFullHistories { get; set; } = new List<ContainerFullHistory>();


    }
}
