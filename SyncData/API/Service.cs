using Retrofit.Net;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncData.API
{
    public class Service
    {

        private static string api = System.Configuration.ConfigurationManager.AppSettings["api"];
        private static RestAdapter adapter = null;
        public static RestAdapter Adapter
        {
            get
            {
                return adapter = new RestAdapter(api);
            }
        }
    }
}
