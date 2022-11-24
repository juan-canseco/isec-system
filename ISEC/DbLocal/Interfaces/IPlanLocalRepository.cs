using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IPlanLocalRepository
    {
        List<PlanLocal> GetAll();
        PlanLocal Get(int id);
        bool Add(PlanLocal planLocal);
        bool Update(PlanLocal planLocal);
        bool Desactivate(int id);
    }
}
