using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.DbLocal.Interfaces
{
    public interface IRolUsuarioRepository
    {
        bool Add(RolUsuarioLocal rolUsuarioLocal);
        bool Update(RolUsuarioLocal rolUsuarioLocal);
        List<RolUsuarioLocal> GetAll();
        List<RolUsuarioLocal> GetAllByRolId(int id);
        List<RolUsuarioLocal> Get(int id);
        int CountByUser(int id);
        bool Delete(int id);
        bool Exists(int rolid, int usuarioid);
        void UpdateSync(List<int> rolUsuarios);

    }
}
