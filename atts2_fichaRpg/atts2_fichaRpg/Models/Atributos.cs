using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.models
{
    [JsonObject(MemberSerialization.OptIn)]
    class Atributos
    {
        public Atributos(int classeId, int forca, int destreza, int inteligencia)
        {
            ClasseId = classeId;
            Forca = forca;
            Destreza = destreza;
            Inteligencia = inteligencia;
        }

        [JsonProperty("classeId")]
        public int ClasseId { get; set; }
        [JsonProperty("forca")]
        public int Forca { get; set; }
        [JsonProperty("destreza")]
        public int Destreza { get; set; }
        [JsonProperty("inteligencia")]
        public int Inteligencia{ get; set; }


    }
}
