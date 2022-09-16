namespace PruebaTecnicaEpik.Models.Models.Request
{
    public class PersonaRequest
    {
        public int Id { get; set; }
        public long Identificacion { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }
        public int Edad { get; set; }
        public int GeneroId { get; set; }
    }
}
