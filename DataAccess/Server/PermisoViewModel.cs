using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Server
{
    public class PermisoViewModel
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public int ModuloId { get; set; }
        public string Modulo { get; set; }
        public bool Acceso { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }
    }
}
