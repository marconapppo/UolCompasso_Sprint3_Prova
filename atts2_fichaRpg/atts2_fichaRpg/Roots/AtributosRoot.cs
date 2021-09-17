using atts2_fichaRpg.models;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.Roots
{
    [JsonObject(MemberSerialization.OptIn)]
    class AtributosRoot : IEnumerable
    {
        public AtributosRoot(IEnumerable<Atributos> atributos)
        {
            Atributos = atributos;
        }

        public IEnumerable<Atributos> Atributos { get; set; }


        public IEnumerator GetEnumerator()
        {
            return Atributos.GetEnumerator();
        }
    }
}
