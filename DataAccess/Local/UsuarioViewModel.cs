using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Local
{
    public class UsuarioViewModel
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public int UsuarioId { get; set; } 
        public int ModuloId { get; set; }
        public string Modulo { get; set; }
        public int Acceso { get; set; }
        public int Lectura { get; set; }
        public int Escritura { get; set; }
    }
}
