using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaEpik.Models.Entities
{
    public class Genero
    {
        public int Id { get; set; }

        [MaxLength(30, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} is obligatorio.")]
        public string Descripcion { get; set; }

        public ICollection<Persona> Personas { get; set; }
    }
}
