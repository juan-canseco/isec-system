using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IGastoLocalRepository
    {
        List<GastoLocal> GetAll();
        List<GastoLocal> GetAllByCobranza(int idCobranza);
        GastoLocal Get(int id);
        bool Add(GastoLocal gastoLocal);
        bool Update(GastoLocal gastoLocal);
        decimal getGastoTotalByCobranza(int idCobranza);

    }
}
