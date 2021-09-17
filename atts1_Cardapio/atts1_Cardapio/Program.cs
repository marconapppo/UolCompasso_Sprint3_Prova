using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using Newtonsoft.Json;

namespace atts1_Cardapio
{
    class Program
    {
        static Cardapio cardapio;
        static bool[] mesaCheia = new bool[4];


        static void Main(string[] args)
        {
            InicializarObjetos();
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

            //valiando Mesa
            int numeroMesa = ValidarMesa();
            Console.Clear();

            //realizando pedido
            Pedido pedido = new Pedido();
            pedido = RealizandoPedido(pedido);

            //finalizando pedido
            FinalizandoPedido(pedido,numeroMesa);

            //convertendo para JSON
            pedido.ListaProduto = pedido.ListaProduto.Distinct().ToList();
            string jsonString = JsonConvert.SerializeObject(pedido, Formatting.Indented);
            Console.WriteLine(jsonString);


            Console.Write("\n\nPressione uma tecla para continuar...");
            Console.ReadKey();
        }


        private static void FinalizandoPedido(Pedido pedido, int numeroMesa)
        {
            Console.Clear();
            Console.WriteLine("A mesa " + numeroMesa + " pediu os seguintes itens:");

            Dictionary<string, int> dicionarioPedido = new Dictionary<string, int>();
            Produto itemAnterior = new Produto(0,"",0.00);
            int count = 1;
            foreach (var item in pedido.ListaProduto)
            {
                if(item.codigo == itemAnterior.codigo)
                {
                    count++;
                    dicionarioPedido[item.descricao] = count;
                }
                else
                { 
                    dicionarioPedido.Add(item.descricao, count);
                }
                itemAnterior = item;
            }
            //print
            foreach(var item in dicionarioPedido)
            {
                Console.WriteLine(item.Value + " - " + item.Key);
            }
            Console.WriteLine("Com valor total de R$: {0:N2}", pedido.valorTotal);

            Console.Write("\n\nPressione uma tecla para continuar...\n");
            Console.ReadKey();
            Console.Clear();
        }

        private static Pedido RealizandoPedido(Pedido pedido)
        {
            int codigo, quantidade;
            Produto produto;
            while (true)
            {
                Console.WriteLine("Cardapio\n");
                cardapio.MostrarCardapio();
                Console.WriteLine("\n");

                Console.Write("Informe o Codigo(encerre o pedido com 999):");
                codigo = Convert.ToInt32(Console.ReadLine());
                //validando codigo
                validarCodigo(codigo);


                if (codigo == 999) { break; }

                Console.Write("Informe a Quantidade:");
                quantidade = Convert.ToInt32(Console.ReadLine());

                produto = cardapio.ConsultarProduto(codigo) as Produto;


                pedido.valorTotal = Math.Round((produto.valorUnitario * (double)quantidade) + pedido.valorTotal);

                while (quantidade > 0)
                {
                    pedido.adicionarObjeto(produto);
                    quantidade--;
                }

                Console.Clear();
            }
            return pedido;
        }

        private static bool validarCodigo(int codigo)
        {
            while (true)
            {
                var produto = cardapio.ConsultarProduto(codigo);
                if (produto == null)
                {
                    Console.WriteLine("Nao eh um codigo valido, digite outro:");
                    codigo = Convert.ToInt32(Console.ReadLine());
                }
                else{
                    break;
                }
            }
            return true;
        }

        private static int ValidarMesa()
        {
            //validando mesa
            Console.WriteLine("Qual o numero da mesa?");
            //-1 pois não existe mesa 0
            int numeroMesa = Convert.ToInt32(Console.ReadLine()) - 1;

            while (true)
            {
                //se a mesa estiver cheia ou não for um numero valido
                if ((numeroMesa > 4) || (numeroMesa < 0))
                {
                    Console.WriteLine("Mesa invalida, digite novamente uma mesa valida:");
                    numeroMesa = Convert.ToInt32(Console.ReadLine());
                }
                else if (mesaCheia[numeroMesa].Equals(true))
                {
                    Console.WriteLine("Mesa cheia, digite novamente uma mesa valida:");
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

        private static void InicializarObjetos()
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
