using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLocadora
{
    public class AppDbContext : DbContext
    {
        public DbSet<Filme> Filmes { get; set; }
        public DbSet<Genero> Generos { get; set; }
        public DbSet<CategoriaFilme> CategoriasFilmes { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Locacao> Locacoes { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var assembly = typeof(AppDbContext).Assembly;

            modelBuilder.Configurations.AddFromAssembly(assembly);
            modelBuilder.Conventions.AddFromAssembly(assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}