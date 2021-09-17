using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts1_Cardapio
{
    class Pedido
    {
        [JsonProperty("ValorTotal")]
        public double valorTotal;
        [JsonProperty("Itens")]
        public List<Produto> ListaProduto = new List<Produto>();


        public void adicionarObjeto(Produto produto)
        {
            ListaProduto.Add(produto);
        }


    }
}
