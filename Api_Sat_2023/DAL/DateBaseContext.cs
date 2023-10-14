using Api_Sat_2023.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace Api_Sat_2023.DAL
{
    public class DateBaseContext : DbContext
    {
        public DateBaseContext(DbContextOptions<DateBaseContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<Country>().HasIndex(c => c.Name).IsUnique(); //Esto es un indice para evitar nombres duplicados +
                                                                             //de países
        }

        public DbSet<Country> Countries { get; set; } // Esta linea me toma la clase country y la mapea en SQL SERVER
                                                      // para crear una tabla de llamadas COUNTRIES
    }
}
