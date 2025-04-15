using Web.Entities;

namespace Web.Models
{
    public class BookingViewModel
    {
        public string UserName { get; set; }
        public Flight Flight { get; set; }
    }
}