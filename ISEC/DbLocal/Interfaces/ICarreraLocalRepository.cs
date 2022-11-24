using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface ICarreraLocalRepository
    {
        List<CarreraLocal> GetAll();
        CarreraLocal Get(int id);
        bool Add(CarreraLocal carreraLocal);
        bool Update(CarreraLocal carreraLocal);
        bool Desactivate(int id);
    }
}
