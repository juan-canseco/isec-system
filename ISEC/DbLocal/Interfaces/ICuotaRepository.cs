using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public  interface ICuotaRepository
    {
        List<CuotaLocal> GetAll();
        CuotaLocal Get(int id);
        bool Add(CuotaLocal cuotaLocal);
        bool Update(CuotaLocal cuotaLocal);
        bool Desactivate(int id); 
    }
}
