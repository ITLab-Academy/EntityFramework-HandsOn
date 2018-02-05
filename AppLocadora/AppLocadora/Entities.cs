using System;
using System.Collections.Generic;
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

        public ICollection<Genero> Generos { get; set; }

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
}