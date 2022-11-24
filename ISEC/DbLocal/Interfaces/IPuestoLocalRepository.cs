using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.DbLocal.Interfaces
{
    public interface IPuestoLocalRepository
    {
        List<PuestoLocal> GetAll();
        PuestoLocal Get(int id);
        bool Add(PuestoLocal puestoLocal);
        bool Update(PuestoLocal puestoLocal);

    }
}
