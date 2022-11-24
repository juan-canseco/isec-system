 
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IAccesoUsuarioLocalRepository
    {
        List<AccesoUsuarioLocal> GetAll();
        List<AccesoUsuarioLocal> GetAllByRol(int rol);
        AccesoUsuarioLocal Get(int id);
        int countByRolId(int id);
        bool Add(AccesoUsuarioLocal accesoUsuarioLocal);
        bool Update(AccesoUsuarioLocal accesoUsuarioLocal);
        bool Delete(int id);
        void UpdateSync(List<int> data);
    }
}
