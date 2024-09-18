namespace TheThanh_WebAPI_RobotHeineken.Data
{
    public class Gift
    {
        public int GiftID { get; set; }
        public string GiftName { get; set; }
        public int IsActive { get; set; }

        public ICollection<QRCode> QRCodes { get; set; } = new List<QRCode>();

    }
}
