using Microsoft.EntityFrameworkCore;
using SyncTests.Models;

namespace SyncTests.Context
{
    public class AppDbContext : DbContext
    {
        public DbSet<Order> Order { get; set; }
        public DbSet<Customer> Customer { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Cambia la cadena de conexión con tus datos de Azure SQL Server
            optionsBuilder
                .UseSqlServer("Server=tcp:netcoremalaga.database.windows.net,1433;Initial Catalog=testmalaga;Persist Security Info=False;User ID=adminmalaga;Password=!x43825721X;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
}
