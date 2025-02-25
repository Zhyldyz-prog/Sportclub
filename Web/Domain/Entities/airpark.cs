using System.ComponentModel.DataAnnotations;

namespace Web.Entities
{
    public class Airpark
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Название аэропарка обязательно")]
        [StringLength(80, ErrorMessage = "Название не должно превышать 80 символов")]
        public string Name { get; set; }

        public List<Airplane>? Airplanes { get; set; }
    }
}

