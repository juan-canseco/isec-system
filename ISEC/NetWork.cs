using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.Modelos
{
    public class NetWork
    { 
        public static  bool IsConnected
        {
            get
            {
                try
                {
                    using (var client = new WebClient())
                    using (client.OpenRead("http://google.com/generate_204"))
                        return true;
                }
                catch
                {
                    return false;
                }
            }
        }
    }
}
