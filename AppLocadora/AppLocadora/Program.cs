using System;
using System.Collections.Generic;
using System.Data.Entity;
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
                contexto.Database.Log = (l) => Console.WriteLine(l);

                var query = contexto.Filmes.Select(t => new
                {
                    t.Id,
                    t.Titulo,
                    Generos = t.Generos.Select(g => g.Nome)
                });

                var resultado = query.ToArray();

                Console.Read();
            }
        }
    }
}
