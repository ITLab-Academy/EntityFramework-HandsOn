using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLocadora
{
    public class FilmeConfiguracao 
        : EntityTypeConfiguration<Filme>
    {
        public FilmeConfiguracao()
        {
            ToTable("Filmes");

            Property(filme => filme.Titulo)
                .HasMaxLength(50)
                .IsRequired();

            Property(filme => filme.Sinopse)
                .HasMaxLength(150);


            HasMany(filme => filme.Generos)
                .WithMany()
                .Map(t => t.ToTable("FilmeGeneros"));
        }
    }

    public class GeneroConfiguracao
        : EntityTypeConfiguration<Genero>
    {
        public GeneroConfiguracao()
        {
            ToTable("Generos");

            Property(genero => genero.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
        }
    }

    public class Convencao : Convention
    {
        public Convencao()
        {
            Properties<string>()
                .Configure(t =>
                {
                    t.HasColumnType("varchar");
                });
        }
    }
}
