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

        public async Task<ClassesRoot> ObterClassesAsync()
        {
            Task<ClassesRoot> t1;
            string jsonStringAtributos = await File.ReadAllTextAsync("classes.json");
            t1 = Task.Run(() => {
                return JsonConvert.DeserializeObject<ClassesRoot>(jsonStringAtributos);
            });
            return await t1;
        }

        public async Task<IdsRoot> ObterIdsFiltradosAsync()
        {
            Task<IdsRoot> t1;
            string jsonStringAtributos = await File.ReadAllTextAsync("ids_filtrados.json");
            t1 = Task.Run(() => {
                return JsonConvert.DeserializeObject<IdsRoot>(jsonStringAtributos);
            });
            return await t1;
        }

        public async Task<AtributosRoot> ObterAtributosDeClasseAsync()
        {
            Task<AtributosRoot> t1;
            string jsonStringAtributos = await File.ReadAllTextAsync("atributos.json");
            t1 = Task.Run(() => {
                return JsonConvert.DeserializeObject<AtributosRoot>(jsonStringAtributos);
            });
            return await t1;
        }
    }
}
