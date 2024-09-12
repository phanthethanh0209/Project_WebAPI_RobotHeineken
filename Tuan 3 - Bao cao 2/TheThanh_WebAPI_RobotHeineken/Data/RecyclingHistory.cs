namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class RecyclingHistory
    {
        public int HistoryID { get; set; }
        public int MachineCode { get; set; }
        public DateTime ApproachTime { get; set; }
        public int HeinekenCans { get; set; }
        public int OtherCans { get; set; }

        // relationship
        public int? MachineID { get; set; }
        public RecyclingMachine RecyclingMachine { get; set; }
        public QRCode QRCode { get; set; }
    }
}
