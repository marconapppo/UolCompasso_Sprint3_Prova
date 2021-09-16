using System;
using System.Collections.Generic;
using System.Linq;

namespace atts1_Cardapio
{
    class Program
    {
        static Cardapio cardapio;
        static bool[] mesaCheia = new bool[4];


        static void Main(string[] args)
        {
            inicializarObjetos();
            bool exibeMenu = true;
            while (exibeMenu)
            {
                exibeMenu = Menu();
            }
        }

        private static bool Menu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Red;

                Console.Write("▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒ PEDIDOS ▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒▒");
            Console.WriteLine(" ");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine("╔═════════════════MENU DE OPÇÕES════════════════╗    ");
            Console.WriteLine("║ 1 EFETUAR PEDIDO                              ║    ");
            Console.WriteLine("║ 2 SAIR                                        ║    ");
            Console.WriteLine("╚═══════════════════════════════════════════════╝    ");

            Console.WriteLine(" ");
            Console.Write("DIGITE UMA OPÇÃO : ");
            
            switch (Console.ReadLine())
             {
                case "1":
                    Pedido();
                    return true;
                case "2":
                    return false;
                default:
                    return true;
            }
        }

        private static void Pedido()
        {
            Console.Clear();
            Console.WriteLine("Pedido\n");

            int numeroMesa = validarMesa();
            Console.Clear();

            int codigo, quantidade;
            Pedido pedido = new Pedido();
            Produto produto;
            while (true)
            {
                Console.WriteLine("Cardapio\n");
                cardapio.mostrarCardapio();
                Console.WriteLine("\n");

                Console.Write("Informe o Codigo(encerre o pedido com 999):");
                codigo = Convert.ToInt32(Console.ReadLine());

                if(codigo == 999) { break; }

                Console.Write("Informe a Quantidade:");
                quantidade = Convert.ToInt32(Console.ReadLine());

                produto = cardapio.consultarProduto(codigo) as Produto;

                pedido.adicionarObjeto(produto, quantidade);
                pedido.valorTotal = (produto.valorUnitario * (double)quantidade) + pedido.valorTotal;

                Console.ReadKey();
                Console.Clear();
            }

            //finalizando pedido
            Console.Clear();
            Console.WriteLine("A mesa " + numeroMesa + " pediu os seguintes itens:");;
                       
            foreach(var item in pedido.ListaProduto)
            {
                Console.WriteLine(item.Value + "-" + item.Key.descricao);
            }
            Console.WriteLine("Com valor total de R$: {0:N2}", pedido.valorTotal);
            

            Console.Write("\n\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }

        private static int validarMesa()
        {
            //validando mesa
            Console.WriteLine("Qual o numero da mesa?");
            //-1 pois não existe mesa 0
            int numeroMesa = Convert.ToInt32(Console.ReadLine()) - 1;

            while (true)
            {
                //se a mesa estiver cheia ou não for um numero valido
                if ((mesaCheia[numeroMesa].Equals(true)) || (numeroMesa > 4) || (numeroMesa < 1))
                {
                    Console.WriteLine("Mesa invalida ou cheia, digite novamente uma mesa valida:");
                    numeroMesa = Convert.ToInt32(Console.ReadLine());
                }
                else
                {
                    mesaCheia[numeroMesa] = true;
                    break;
                }
            }
            return numeroMesa;
             
        }

        private static void inicializarObjetos()
        {

            Produto[] p = new Produto[] {
                new Produto(100, "Cachorro quente", 5.70),
                new Produto(101, "X Completo", 18.30),
                new Produto(102, "X Salada", 16.50),
                new Produto(103, "Hamburguer", 22.40),
                new Produto(104, "Coca 2L", 10.00),
                new Produto(105, "Refrigerante", 1.00)
            };
            

            var listaProduto = new List<Produto>();
            listaProduto.AddRange(p);


            cardapio = new Cardapio(listaProduto);

        }
    }      

}
