using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IModuloLocalRepository
    {
        List<ModuloLocal> GetAll();
        ModuloLocal Get(int id);
        ModuloLocal Get(ModuloLocal id);
        bool Add(ModuloLocal moduloLocal);
        bool Update(ModuloLocal moduloLocal);
        bool Exists(string nombre);
      
    }
}
