using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLocadora
{
    public class Filme
    {
        public Filme()
        {
            Generos = new List<Genero>();
        }

        public int Id { get; set; }
        public string Titulo { get; set; }
        public string Sinopse { get; set; }

        public CategoriaFilme Categoria { get; set; }
        public ICollection<Genero> Generos { get; set; }
        public ICollection<LocacaoFilme> Locacoes { get; set; }

        public void AdicionarGenero(Genero genero)
        {
            if (Generos.Any(g => g.Id == genero.Id))
                throw new InvalidOperationException("Genero já cadastrado para o filme");

            Generos.Add(genero);
        }
    }

    public class Genero
    {
        public int Id { get; set; }
        public string Nome { get; set; }
    }

    public enum Categoria
    {
        Lancamento = 1,
        Recente = 2,
        Classico = 3
    }

    public class CategoriaFilme
    {
        public byte Id { get; set; }
        public string Nome { get; set; }
        public string Cor { get; set; }
        public decimal Preco { get; set; }
    }

    public class Cliente
    {
        public Cliente()
        {
            Locacoes = new List<Locacao>();
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }

        public ICollection<Locacao> Locacoes { get; set; }
    }

    public class Locacao : IValidatableObject
    {
        public Locacao()
        {
            Encargos = new List<Encargo>();
            Filmes = new List<LocacaoFilme>();

            DataLocacao = DateTime.Now;
        }

        public int Id { get; set; }
        public DateTime DataLocacao { get; set; }
        public DateTime DataDevolucaoPrevista { get; set; }
        public ICollection<Encargo> Encargos { get; set; }

        public Cliente Cliente { get; set; }
        public ICollection<LocacaoFilme> Filmes { get; set; }

        public void AdicionarDesconto(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException("Valor de desconto inválido");

            AdicionarEncargo(valor * -1);
        }

        public void AdicionarAcrescimo(decimal valor)
        {
            if (valor <= 0)
                throw new InvalidOperationException("Valor de acrescimo inválido");

            AdicionarEncargo(valor);
        }

        private void AdicionarEncargo(decimal valor)
        {
            Encargos.Add(new Encargo
            {
                Valor = valor
            });
        }

        internal void AdicionarFilme(Filme filme)
        {
            Filmes.Add(new LocacaoFilme
            {
                Filme = filme,
                Preco = filme.Categoria.Preco
            });
        }

        public decimal ValorTotal()
        {
            return Filmes.Sum(t => t.Preco) + Encargos.Sum(t => t.Valor);
        }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var resultadoValidacao = new List<ValidationResult>();

            if (Filmes.Any() == false) {
                resultadoValidacao.Add(new ValidationResult("Não é possível gerar locação sem filmes"));
            }

            if (!(DataDevolucaoPrevista > DataLocacao))
                resultadoValidacao.Add(new ValidationResult("Data de devolução prevista inválida"));

            return resultadoValidacao;
        }
    }

    public enum TipoEncargo
    {
        Acrescimo = 1,
        Desconto = 2
    }

    public class Encargo
    {
        public int Id { get; set; }
        public decimal Valor { get; set; }
        public string Referencia { get; set; }
    }

    public class LocacaoFilme
    {
        public int LocacaoId { get; set; }
        public int FilmeId { get; set; }


        public Filme Filme { get; set; }
        public decimal Preco { get; set; }
        public DateTime? DataDevolucao { get; set; }
    }
}