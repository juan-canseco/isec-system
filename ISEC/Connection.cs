using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;
using System.Configuration;
namespace ISEC.Modelos
{
    public  class Connection
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
            return ConfigurationManager.ConnectionStrings["dbISEC"].ConnectionString;
        }
    }
}
