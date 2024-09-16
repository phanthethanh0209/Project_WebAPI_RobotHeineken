namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class ContainerFullHistory
    {
        public int ContainerID { get; set; }
        public DateTime FullTime { get; set; }
        public int TotalHeinekenCans { get; set; }
        public int TotalOtherCans { get; set; }

        public int MachineID { get; set; }
        public RecyclingMachine RecyclingMachine { get; set; }

    }
}
