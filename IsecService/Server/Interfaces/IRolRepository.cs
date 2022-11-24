using System;
using System.Collections.Generic; 
using DataAccess.Server;
using DataAccess.Local;
namespace IsecService.Server.Interfaces
{
    public interface IRolRepository
    {
        List<RolServer> GetAll();
        bool Exists(string rol);
        int Last();
        int Add(RolServer rol);
        List<int> RolesNoSync(List<RolLocal> rolesLocales);
        int AddRolInserted(RolLocal rolLocal);
        int AddRolUpdated(RolLocal rolLocal);
    }
}
