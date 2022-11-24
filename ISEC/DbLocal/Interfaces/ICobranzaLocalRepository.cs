using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local; 
namespace ISEC.DbLocal.Interfaces
{
    public interface ICobranzaLocalRepository
    {
        List<CobranzaLocal> All();
        int GetLastFolio();
        CobranzaLocal Get(int id);
        CobranzaLocal GetLastCobranza();
        int GetLast();
        bool Add(CobranzaLocal cobranzaLocal);
        bool Update(CobranzaLocal cobranzaLocal);
        bool Delete(int id);
        bool SumQuantity(decimal quantity);
        bool RestaQuantity(decimal quantity,int id);
        int Exists(); 
    }
}
