using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.models
{
    [JsonObject(MemberSerialization.OptIn)]
    class IdsRoot
    {
        public IdsRoot(IEnumerable<int> ids)
        {
            Ids = ids;
        }

        public IEnumerable<int> Ids { get; set; }

    }
}
