using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.Models
{
    class Ficha
    {
        public Ficha(int id, string nomeClasse, int forca, int destreza, int inteligencia)
        {
            Id = id;
            NomeClasse = nomeClasse;
            Forca = forca;
            Destreza = destreza;
            Inteligencia = inteligencia;
        }

        public int Id { get; set; }
        public string NomeClasse { get; set; }
        public int Forca { get; set; }
        public int Destreza { get; set; }
        public int Inteligencia { get; set; }
    }
}
