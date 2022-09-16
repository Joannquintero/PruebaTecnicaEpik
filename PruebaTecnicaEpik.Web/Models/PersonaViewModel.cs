using PruebaTecnicaEpik.Models.Entities;
using System.ComponentModel.DataAnnotations;

namespace PruebaTecnicaEpik.Web.Models
{
    public class PersonaViewModel
    {
        public int Id { get; set; }
        public long Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public Genero Genero { get; set; }
        public int GeneroId { get; set; }
    }
}
