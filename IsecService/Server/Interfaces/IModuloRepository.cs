using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Local;
using DataAccess.Server;
namespace IsecService.Server.Interfaces
{
    public interface IModuloRepository
    {
        List<ModuloServer> GetAll();
        List<int> ModulosNoSync(List<ModuloLocal> modulosLocal);
        int AddModuloInserted(ModuloLocal moduloLocal);
        int AddModuloUpdated(ModuloLocal moduloLocal);
    }
}
