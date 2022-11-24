using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Server;
using DataAccess;

namespace IsecService.Server.Interfaces
{
    public interface IAccesoUsuarioRepository
    {
        List<AccesoUsuarioServer> GetAll();
        List<AccesoUsuarioServer> GetAllByRol(int rol);
        void Updated(AccesoUsuarioServer permiso);
    }
}
