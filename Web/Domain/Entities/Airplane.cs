using System.ComponentModel.DataAnnotations;

namespace Web.Entities
{
    public class Airplane
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название самолета обязательно")]
        [StringLength(50, ErrorMessage = "Название не должно превышать 50 символов")]
        public string Name { get; set; }

        [Required]
        public int AirparkId { get; set; }
        public Airpark? Airpark { get; set; } 


        [Required(ErrorMessage = "Введите номер борта")]
        [RegularExpression(@"^[A-Z0-9-]+$", ErrorMessage = "Номер борта должен содержать только заглавные буквы, цифры и дефис")]
        public string Airnumber { get; set; }

        [Required(ErrorMessage = "Введите регистрационный номер")]
        [StringLength(6, ErrorMessage = "Регистрационный номер не должен превышать 6 символов")]
        public string Regisnumber { get; set; }

        [Required(ErrorMessage = "Введите вместимость")]
        [Range(1, 100, ErrorMessage = "Вместимость должна быть от 1 до 100 пассажиров")]
        public int Vmestimost { get; set; }


        public List<Flight>? Flights { get; set; }
    }
}