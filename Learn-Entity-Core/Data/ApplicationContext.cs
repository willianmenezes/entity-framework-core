using Learn_Entity_Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Learn_Entity_Core.Data
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Pedido> Pedidos { get; set; }
        public DbSet<Produto> Produtos { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder
                .LogTo(x => Console.WriteLine(x)) // habilita os logs
                .EnableSensitiveDataLogging() // mostra dados sensiveis das consulta (parametros)
                .UseSqlServer("Data Source = (localdb)\\MSSQLLocalDB; Initial Catalog = master; Integrated Security = True;");
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            ConfigurarPropiedadesNaoMapeadas(modelBuilder);
            base.OnModelCreating(modelBuilder);
        }

        private void ConfigurarPropiedadesNaoMapeadas(ModelBuilder modelBuilder)
        {
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var properties = entity.GetProperties().Where(p => p.ClrType == typeof(string));

                foreach (var property in properties)
                {
                    if (string.IsNullOrWhiteSpace(property.GetColumnType()) && !property.GetMaxLength().HasValue)
                    {
                        property.SetColumnType("VARCHAR(100)");
                    }
                }
            }
        }
    }
}
