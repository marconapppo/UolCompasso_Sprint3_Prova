using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts1_Cardapio
{
    class Produto
    {
        [JsonProperty("Codigo")]
        public int codigo;
        [JsonProperty("Descricao")]
        public string descricao;
        [JsonIgnore]
        public double valorUnitario;

        public Produto()
        {

        }

        public Produto(int codigo, string descricao, double valorUnitario)
        {
            this.codigo = codigo;
            this.descricao = descricao;
            this.valorUnitario = valorUnitario;
        }

    }
}
