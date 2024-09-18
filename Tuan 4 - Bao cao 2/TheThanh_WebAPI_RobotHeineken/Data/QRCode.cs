namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class QRCode
    {
        public int Code { get; set; }
        public int IsActive { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime? TimeUsed { get; set; }


        // relationship
        public int HistoryID { get; set; }
        public RecyclingHistory RecyclingHistory { get; set; }

        public int GiftID { get; set; }
        public Gift Gift { get; set; }

        public int? UserID { get; set; }
        public User User { get; set; }

    }
}
