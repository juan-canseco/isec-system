using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IGrupoLocalRepository
    {
        List<GrupoLocal> GetAll();
        GrupoLocal Get(int id);
        bool Add(GrupoLocal grupoLocal);
        bool Update(GrupoLocal grupoLocal);
        bool Desactivate(int id);

    }
}
