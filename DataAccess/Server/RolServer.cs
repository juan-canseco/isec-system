using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Server
{
    public class RolServer
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Sync { get; set; }
        public int Activo { get; set; }
        public DateTime? LastUpdate { get; set; }
    }
}
