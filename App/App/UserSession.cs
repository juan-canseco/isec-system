using System;
using System.Collections.Generic;
using System.Text;
using DataAccess;
namespace App
{
    public class UserSession
    {
        public Usuario UsuarioActual { get; set; }
        private static UserSession _Instancia = null; 
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
