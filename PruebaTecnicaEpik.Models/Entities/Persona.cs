using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaEpik.Models.Entities
{
    public class Persona
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public long Identificacion { get; set; }

        [MaxLength(64, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Nombres { get; set; }

        [MaxLength(64, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Apellidos { get; set; }

        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public int Edad { get; set; }

        public Genero Genero { get; set; }
    }
}
