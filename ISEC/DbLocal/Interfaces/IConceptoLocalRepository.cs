using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface IConceptoLocalRepository
    {
        List<ConceptoLocal> GetAll();
        ConceptoLocal Get(int id);
        bool Add(ConceptoLocal conceptoLocal);
        bool Update(ConceptoLocal conceptoLocal); 
    }
}
