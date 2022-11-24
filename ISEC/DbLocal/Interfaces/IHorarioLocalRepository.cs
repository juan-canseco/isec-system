using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IHorarioLocalRepository
    {
        List<HorarioLocal> GetAll();
        HorarioLocal Get(int id);
        bool Add(HorarioLocal horarioLocal);
        bool Update(HorarioLocal horarioLocal); 
        bool Desactivate (int id);
    }
}
