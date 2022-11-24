using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Server
{
    public class RolUsuarioServer
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public int UsuarioId { get; set; }
        public string Usuario { get; set; }
        public int Sync { get; set; }
    }
}
