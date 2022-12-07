using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.DbLocal.Interfaces
{
    public interface IArqueoLocalRepository
    {

        List<ArqueoLocal> GetAllByFolio(string folio);
        List<ArqueoLocal> GetAll();
        ArqueoLocal Get(int id);
        bool Add(ArqueoLocal arqueoLocal);
        bool Update(ArqueoLocal arqueoLocal);
        bool Desactive(ArqueoLocal arqueoLocal);
        int GenerateFolio();
    }
}
