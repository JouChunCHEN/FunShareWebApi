namespace FunShareWebApi.Models
{
    public class CEventListForApp
    {
        public int ProductDetail_ID { get; set; }
        public string ProductName { get; set; }
        public DateTime BeginTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Address { get; set; }
        public string ImageFileName { get; set; }
        public int Stock { get; set; }
    }
}
