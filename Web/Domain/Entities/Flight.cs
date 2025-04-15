using System.ComponentModel.DataAnnotations;

namespace Web.Entities
{
    public class Flight
    {
        public int Id { get; set; }

        [Required]
        public string Country { get; set; }

        [Required]
        public DateTime DepartureTime { get; set; }

        [Required]
        public string Class { get; set; } // Эконом, Бизнес

        [Required]
        public string Baggage { get; set; } // Например: 1 место (23кг)

        public int AirplaneId { get; set; }
        public Airplane? Airplane { get; set; }


    }
}