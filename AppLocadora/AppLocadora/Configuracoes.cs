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

            HasRequired(t => t.Categoria)
                .WithMany();
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

    public class CategoriaFilmeConfiguracao
        : EntityTypeConfiguration<CategoriaFilme>
    {
        public CategoriaFilmeConfiguracao()
        {
            ToTable("CategoriasFilme");

            HasKey(t => t.Id);

            Property(t => t.Id)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            Property(t => t.Nome)
                .HasMaxLength(20)
                .IsRequired();

            Property(t => t.Cor)
                .HasMaxLength(7)
                .IsRequired()
                .HasColumnType("char");

            Property(t => t.Preco)
                .HasPrecision(precision: 10, scale: 2);

        }
    }

    public class ClienteConfiguration
        : EntityTypeConfiguration<Cliente>
    {
        public ClienteConfiguration()
        {
            ToTable("Clientes");

            Property(t => t.Nome)
                .HasMaxLength(80)
                .IsRequired();

            Property(t => t.Telefone)
                .HasMaxLength(11)
                .IsRequired();
        }
    }

    public class EncargoConfiguration 
        : EntityTypeConfiguration<Encargo>
    {
        public EncargoConfiguration()
        {
            ToTable("LocacaoEncargos");

            Property(t => t.Valor)
                .HasPrecision(precision: 10, scale: 2);

            Property(t => t.Referencia)
                .HasMaxLength(200);
        }
    }

    public class LocacaoFilmeConfiguration
        : EntityTypeConfiguration<LocacaoFilme>
    {
        public LocacaoFilmeConfiguration()
        {
            ToTable("LocacoesFilmes");

            HasKey(t => new { t.LocacaoId, t.FilmeId });

            Property(t => t.Preco)
                .HasPrecision(precision: 10, scale: 2);
        }
    }

    public class LocacaoConfiguracao
        : EntityTypeConfiguration<Locacao>
    {
        public LocacaoConfiguracao()
        {
            ToTable("Locacoes");

            HasMany(t => t.Encargos)
                .WithRequired();

            HasRequired(t => t.Cliente)
                .WithMany(t => t.Locacoes);

            HasMany(t => t.Filmes)
                .WithRequired();
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
