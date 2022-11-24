using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Local
{
    public class RolLocal
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Sync { get; set; }
        public bool Sincronizado
        {
            get
            {
                return Sync > 0 ? true : false;
            }
        }
        public int Activo { get; set; }
        public bool EsActivo
        {
            get
            {
                return Activo > 0 ? true : false;
            }
        }
    }
}
