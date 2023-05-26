using Microsoft.EntityFrameworkCore;
using TodoParaTuPerro.Models;

namespace TodoParaTuPerro.Controllers.Datos
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        public DbSet<Pedidos> Pedidos { get; set; }
        public DbSet<Paqueterias> Paqueterias { get; set; }
        public DbSet<Empleados> Empleados { get; set; }
    }
}