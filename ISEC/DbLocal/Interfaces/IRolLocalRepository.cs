using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local; 
namespace ISEC.DbLocal.Interfaces
{
    public interface IRolLocalRepository
    {
        List<RolLocal> GetAll();
        List<RolLocal> GetAllNoSync();
        RolLocal Get(int id); 
        int GetLast();
        bool Add(RolLocal rolLocal);
        bool Update(RolLocal rolLocal);
        bool Desactivate(int id);
        bool Exists(string rol);
        void UpdateSync(List<int> roles);

    }
}
