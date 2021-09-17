using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.models
{
    [JsonObject(MemberSerialization.OptIn)]
    class Classe
    {
        public Classe(int id, string nomeClasse, Atributos atributos)
        {
            Id = id;
            NomeClasse = nomeClasse;
            Atributos = atributos;
        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("nomeClasse")]
        public string NomeClasse { get; set; }

        public Atributos Atributos { get; set; }
    }
}
