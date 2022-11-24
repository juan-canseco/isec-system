using DataAccess.Local;
using ISEC.Modelos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace ISEC
{
    public  class UserSession
    {
        private static UserSession _Instancia = null;
        public UsuarioLocal Usuario { get; set; } 
        public UsuarioLocal UsuarioAccion { get; set; }
        public CobranzaLocal Cobranza { get; set; }
        public bool IsUpdate { get; set; }
        public static UserSession Instancia
        {
            get
            {
                if (_Instancia == null)
                    _Instancia = new UserSession();
                return _Instancia;
            }
        }

    }
}
