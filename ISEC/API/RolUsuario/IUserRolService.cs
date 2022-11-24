using DataAccess.Server;
using RestSharp;
using Retrofit.Net.Attributes.Methods;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.API.RolUsuario
{
    public interface IUserRolService
    {
        [Get("userrole/all/")]
        RestResponse<List<RolUsuarioServer>> all();
    }
}
