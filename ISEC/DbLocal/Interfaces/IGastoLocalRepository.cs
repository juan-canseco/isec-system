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
        GastoLocal Get(int id);
        bool Add(GastoLocal gastoLocal);
        bool Update(GastoLocal gastoLocal);

    }
}
