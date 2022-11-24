using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.DbLocal.Interfaces
{
    public  interface IClaveLocalRepository
    {
        List<ClaveLocal> GetAll();
        ClaveLocal Get(int id);
        ClaveLocal GetByClave(string clave);
        bool Add(ClaveLocal claveLocal);
        bool Update(ClaveLocal claveLocal);
        bool Desactivate(int id);
    }
}
