using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.ViewModels
{
    public  class RolViewModel
    {
        public string Nombre { get; set; }
        public List<ModuloLocal> Modulos { get; set; }
        public bool Acceso { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

    }
}
