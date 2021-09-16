using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts1_Cardapio
{
    class Produto
    {
        public int codigo;
        public string descricao;
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
