using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using atts2_fichaRpg.Interfaces;
using atts2_fichaRpg.models;
using atts2_fichaRpg.Roots;
using Newtonsoft.Json;

namespace atts2_fichaRpg
{
    class Program : IDataService
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            //somente testes por enquanto
            string jsonString = File.ReadAllText("atributos.json");

            var teste = JsonConvert.DeserializeObject<AtributosRoot>(jsonString);


            ObterAtributosDeClasseAsync();
            //foreach(var t in teste)
            //{
            //    Console.WriteLine(t.ClasseId);
            //}


        }

        public Task<IEnumerable<Atributos>> ObterAtributosDeClasseAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Classe>> ObterClassesAsync()
        {
            //lendo do arquivo JSON

            throw new NotImplementedException();
        }

        public Task<IEnumerable<int>> ObterIdsFiltradosAsync()
        {
            throw new NotImplementedException();
        }
    }
}
