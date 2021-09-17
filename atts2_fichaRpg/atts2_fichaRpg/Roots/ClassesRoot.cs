using atts2_fichaRpg.models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.Roots
{
    [JsonObject(MemberSerialization.OptIn)]
    class ClassesRoot
    {
        public IEnumerable<Classe> Classes { get; set; }

        public ClassesRoot(IEnumerable<Classe> classes)
        {
            Classes = classes;
        }
    }
}
