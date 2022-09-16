using Microsoft.EntityFrameworkCore;
using PruebaTecnicaEpik.Models.Entities;

namespace PruebaTecnicaEpik.Api.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) { }

        public DbSet<Persona> Personas { get; set; }
        public DbSet<Genero> Generos { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Persona>().HasIndex(x => x.Identificacion).IsUnique();
        }
    }
}
