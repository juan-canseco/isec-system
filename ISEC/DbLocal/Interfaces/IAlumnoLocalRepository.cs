using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public  interface IAlumnoLocalRepository
    {
        List<AlumnoLocal> GetAll();
        AlumnoLocal Get(int id);
        AlumnoLocal GetByCredencial(string credencial);
        AlumnoLocal GetLast();
        bool Add(AlumnoLocal alumnoLocal);
        bool Update(AlumnoLocal alumnoLocal);
        bool Desactivate(int id);
    }
}
