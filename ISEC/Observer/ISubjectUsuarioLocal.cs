using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.Observer
{
    public interface ISubjectUsuarioLocal
    { 
        void Notify();
        void Update();
    }
}
