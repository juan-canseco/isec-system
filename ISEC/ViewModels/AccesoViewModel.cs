using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.ViewModels
{
    public class AccesoViewModel
    {
        public int ModuloId { get; set; }
        public string  Nombre { get; set; }
        public bool  Acceso { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

    }
}
