using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace IsecService.Models
{
    public class Connection
    { 
        private static Connection _Instance = null;
        public static Connection Instance
        {
            get
            {
                if (_Instance == null)
                    _Instance = new Connection();
                return _Instance;
            }
        }
        public string ConnectionString()
        {
            return ConfigurationManager.ConnectionStrings["dbISECServer"].ConnectionString;
        }
    }
}