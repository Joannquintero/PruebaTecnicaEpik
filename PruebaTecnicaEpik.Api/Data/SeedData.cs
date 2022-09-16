using PruebaTecnicaEpik.Models.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaTecnicaEpik.Api.Data
{
    public class SeedData
    {
        private readonly DataContext _context;

        public SeedData(DataContext context)
        {
            _context = context;
        }

        public async Task SeedAsync()
        {
            await _context.Database.EnsureCreatedAsync();
            await CheckGenerosAsync();
        }

        private async Task CheckGenerosAsync()
        {
            if (!_context.Generos.Any())
            {
                _context.Generos.Add(new Genero { Descripcion = "Masculino" });
                _context.Generos.Add(new Genero { Descripcion = "Femenino" });
                await _context.SaveChangesAsync();
            }
        }
    }
}
