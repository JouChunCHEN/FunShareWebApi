namespace FunShareWebApi.Models
{
    public class CAttendList
    {
        public int OrderDetailId { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string odNumber { get; set; }
        public bool? IsAttend { get; set; }
    }
}
