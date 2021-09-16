using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts1_Cardapio
{
    class Pedido
    {
        public double valorTotal;
        public Dictionary<Produto,int> ListaProduto = new Dictionary<Produto, int>();

        public void adicionarObjeto(Produto produto,int quantidade)
        {
            ListaProduto.Add(produto,quantidade);
        }
    }
}
