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
        static async Task Main(string[] args)
        { 
            Program p = new Program();

            //tasks
            Task<AtributosRoot> atributosEnumerable = null;
            Task<ClassesRoot> classesEnumerable = null;
            Task<IdsRoot> idsEnumerable = null;

            //obtendo valor de jSON com paralelismo
            Parallel.Invoke( () => atributosEnumerable = p.ObterAtributosDeClasseAsync(),
                        () => classesEnumerable = p.ObterClassesAsync(),
                        () => idsEnumerable = p.ObterIdsFiltradosAsync()
                );

            //esperando as tasks acabarem
            atributosEnumerable.Wait();
            classesEnumerable.Wait();
            idsEnumerable.Wait();

            List<Classe> listaClasse = new List<Classe>();
            Parallel.ForEach(classesEnumerable.Result.Classes, c => {
                Parallel.ForEach(idsEnumerable.Result.Ids, i =>
                {
                    if(c.Id == i)
                    {
                        lock (listaClasse)
                        {
                            var id = c;
                            listaClasse.Add(id);
                        }
                    }
                });
            });

            List<Ficha> listaFicha = new List<Ficha>();
            Parallel.ForEach(classesEnumerable.Result.Classes, c => {
                Parallel.ForEach(atributosEnumerable.Result.Atributos, a =>{
                    if(c.Id == a.ClasseId)
                    {
                        lock (listaFicha)
                        { 
                            listaFicha.Add(new Ficha(c.Id, c.NomeClasse, a.Forca, a.Destreza, a.Inteligencia));
                        }
                    }
                });
            });

            foreach(var item in listaFicha)
            {
                Console.WriteLine(@$"       ----    ----        ---         ");
                Console.WriteLine(@$"Id: {item.Id}                          ");
                Console.WriteLine(@$"Nome: {item.NomeClasse}                ");
                Console.WriteLine(@$"      Atributos                        ");
                Console.WriteLine(@$"FOR: {item.Forca}            ");
                Console.WriteLine(@$"DES: {item.Destreza}         ");
                Console.WriteLine(@$"INT: {item.Inteligencia}     ");
                Console.WriteLine(@"                                        ");
            }
            
        }


        public async Task<ClassesRoot> ObterClassesAsync()
        {
            string jsonStringAtributos = await File.ReadAllTextAsync("classes.json");
            var classe = JsonConvert.DeserializeObject<ClassesRoot>(jsonStringAtributos);
            return classe;
        }

        public async Task<IdsRoot> ObterIdsFiltradosAsync()
        {
            string jsonStringAtributos = await File.ReadAllTextAsync("ids_filtrados.json");
            var ids = JsonConvert.DeserializeObject<IdsRoot>(jsonStringAtributos);
            return ids;
        }

        public async Task<AtributosRoot> ObterAtributosDeClasseAsync()
        {
            Task<AtributosRoot> t1;
            string jsonStringAtributos = await File.ReadAllTextAsync("atributos.json");
            var atributos = JsonConvert.DeserializeObject<AtributosRoot>(jsonStringAtributos);
            return atributos;
        }
    }
}
