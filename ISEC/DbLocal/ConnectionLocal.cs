using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 
namespace ISEC.DbLocal
{
    public  class ConnectionLocal
    {
        public static string ConecctionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["dbIsecLocal"].ConnectionString; 
            }
        }

    }
}
