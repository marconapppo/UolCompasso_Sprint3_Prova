using atts2_fichaRpg.models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.Interfaces
{
    interface IDataService
    {
        Task<IEnumerable<Classe>> ObterClassesAsync();
        Task<IEnumerable<int>> ObterIdsFiltradosAsync();
        Task<IEnumerable<Atributos>> ObterAtributosDeClasseAsync();
    }
}
