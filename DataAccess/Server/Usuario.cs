using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
    public class Usuario
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int puestoid { get; set; } 
        public string Puesto { get; set; }
        public int Sync { get; set; }
        public int Activo { get; set; } 
        public DateTime? LastUpdate { get; set; }
        public string Rol { get; set; }
    }
}
