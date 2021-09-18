using atts2_fichaRpg.models;
using atts2_fichaRpg.Roots;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace atts2_fichaRpg.Interfaces
{
    interface IDataService
    {
        Task<ClassesRoot> ObterClassesAsync();
        Task<IdsRoot> ObterIdsFiltradosAsync();
        Task<AtributosRoot> ObterAtributosDeClasseAsync();
    }
}
