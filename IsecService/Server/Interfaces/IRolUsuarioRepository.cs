using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Server;
namespace IsecService.Server.Interfaces
{
    public interface IRolUsuarioRepository
    {
        List<RolUsuarioServer> GetAllByRol(int id);
        int Last();
        int existe(int rol, int user);
        int Add(RolUsuarioServer rolUsuarioServer);
    }
}
