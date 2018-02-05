namespace AppLocadora.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<AppLocadora.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppLocadora.AppDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.

            context.Generos.AddOrUpdate(t => t.Id,
                new Genero { Id = 1, Nome = "A��o" },
                new Genero { Id = 2, Nome = "Policial" },
                new Genero { Id = 3, Nome = "Terror" });
        }
    }
}
