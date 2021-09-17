using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts1_Cardapio
{
    class Cardapio
    {
        private List<Produto> ListaProduto;

        public Cardapio()
        {

        }

        public Cardapio(List<Produto> ListaProduto)
        {
            this.ListaProduto = ListaProduto;
        }

        public void MostrarCardapio()
        {
            Console.WriteLine("Codigo\t Produto\t Preço Unitario");
            foreach (var produto in ListaProduto)
            {
                Console.WriteLine(produto.codigo + "\t " + produto.descricao + "\t R$:{0:N2}", produto.valorUnitario);
            }
        }

        public Produto ConsultarProduto(int codigo)
        {
            var produtos = from p in ListaProduto
                        where p.codigo == codigo
                        select p;
            //transforma para 1 objeto
            foreach(var produto in produtos)
            {
                return produto;
            }
            return null;
        }
    }
}
