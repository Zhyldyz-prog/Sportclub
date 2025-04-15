using System.ComponentModel.DataAnnotations;

namespace Web.Entities
{
    public class Booking
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя пассажира")]
        public string PassengerName { get; set; }

        [Required]
        [DataType(DataType.Date)]
        public DateTime FlightDate { get; set; }

        [Required(ErrorMessage = "Выберите самолёт")]
        public int AirplaneId { get; set; }

        public Airplane? Airplane { get; set; }
    }
}