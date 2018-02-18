using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppLocadora
{
    class Program
    {
        static void Main(string[] args)
        {
            Database.SetInitializer<AppDbContext>(null);

            using (var contexto = new AppDbContext())
            {
                contexto.Database.Log = (l) =>
                {
                    Debug.WriteLine(l);
                    Console.WriteLine(l);
                };

                var ricardo = contexto.Clientes.Include(t => t.Locacoes)
                                               .Include(t => t.Locacoes.Select(tt => tt.Filmes))
                                               .FirstOrDefault(t => t.Nome == "Ricardo");

                var ricardoTaDevendo = ricardo.Locacoes.SelectMany(t => t.Filmes)
                                                       .Any(t => t.DataDevolucao == null);

                if (ricardoTaDevendo)
                {
                    Console.WriteLine("Ricardo tá devendo!!!!!!");
                }

                var locacaoFilmes = contexto.Filmes.Select(t => new
                {
                    t.Titulo,
                    Quantidade = t.Locacoes.Count()
                });

                foreach (var locacaoFilme in locacaoFilmes)
                {
                    Console.WriteLine("Filme: {0}, Quantidade: {1}", locacaoFilme.Titulo, locacaoFilme.Quantidade);
                }

                Console.Read();


                //CriarLocacao(contexto);
                //CriarClientes(contexto);
                //CriarFilmes(contexto);
            }
        }

        private static void CriarLocacao(AppDbContext contexto)
        {
            var ricardo = contexto.Clientes.FirstOrDefault(x => x.Nome == "Ricardo");
            var filme = contexto.Filmes.Include(t => t.Categoria)
                                       .FirstOrDefault();

            var locacao = new Locacao();
            locacao.Cliente = ricardo;
            locacao.AdicionarFilme(filme);
            locacao.DataLocacao = DateTime.Now;
            locacao.DataDevolucaoPrevista = DateTime.Today.AddDays(2);

            Console.WriteLine("O Ricardo vai pagar: {0}", locacao.ValorTotal());

            contexto.Locacoes.Add(locacao);
            contexto.SaveChanges();
        }

        private static void CriarClientes(AppDbContext contexto)
        {
            var ricardo = new Cliente { Nome = "Ricardo", Telefone = "1111111111" };
            var tiago = new Cliente { Nome = "Tiago", Telefone = "2222222222" };

            contexto.Clientes.AddRange(new[] { ricardo, tiago });
            contexto.SaveChanges();
        }

        private static void CriarFilmes(AppDbContext contexto)
        {
            var categoriaRecente = contexto.CategoriasFilmes.Attach(new CategoriaFilme
            {
                Id = (byte)Categoria.Recente
            });

            var categoriaLancamento = contexto.CategoriasFilmes.Attach(new CategoriaFilme
            {
                Id = (byte)Categoria.Lancamento
            });

            var batmanBegins = new Filme
            {
                Titulo = "Batman Begins",
                Categoria = categoriaRecente
            };

            var oUltimoJedi = new Filme
            {
                Titulo = "Star Wars - O Último Jedi",
                Categoria = categoriaLancamento
            };


            contexto.Filmes.AddRange(new[] { batmanBegins, oUltimoJedi });

            contexto.SaveChanges();
        }
    }
}
