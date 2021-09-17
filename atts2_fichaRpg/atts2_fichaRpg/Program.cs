using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;
using atts2_fichaRpg.Interfaces;
using atts2_fichaRpg.models;
using atts2_fichaRpg.Models;
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
            Teste();

        }

        static void Teste()
        {
            string jsonStringAtributos = File.ReadAllText("atributos.json");
            string jsonStringClassses = File.ReadAllText("classes.json");
            string jsonStringIds = File.ReadAllText("ids_filtrados.json");

            var atributosTeste = JsonConvert.DeserializeObject<AtributosRoot>(jsonStringAtributos);
            var classesTeste = JsonConvert.DeserializeObject<ClassesRoot>(jsonStringClassses);
            var idsTeste = JsonConvert.DeserializeObject<IdsRoot>(jsonStringIds);

            //adicionando em nova lista classes + ids
            List<Classe> listaClasse = new List<Classe>();
            foreach (var c in classesTeste.Classes)
            {
                foreach(var i in idsTeste.Ids)
                {
                    if (c.Id == i)
                    {
                        listaClasse.Add(c);
                    }
                }
            }
            //adicionando em nova lista atributos + classes
            List<Ficha> listaFicha = new List<Ficha>();
            foreach(var c in classesTeste.Classes)
            {
                foreach(var a in atributosTeste.Atributos)
                {
                    if(c.Id == a.ClasseId)
                    {
                        listaFicha.Add(new Ficha(c.Id,c.NomeClasse,a.Forca,a.Destreza,a.Inteligencia));
                    }
                }
            }

            foreach(var t in listaFicha)
            {
                Console.WriteLine(t.Id + " " + t.NomeClasse + " " + t.Forca);
            }
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
