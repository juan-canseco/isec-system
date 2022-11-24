using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
using DataAccess.Server;
using RestSharp;
using Retrofit.Net.Attributes.Methods;
using Retrofit.Net.Attributes.Parameters; 
namespace ISEC.API.Roles
{
    public interface IRolService
    {
        [Get("roles/all/")]
        RestResponse<List<RolServer>> All();

        [Post("roles/sync/")]
        RestResponse<List<int>> sync([Body] List<RolLocal> roles);

        [Post("roles/syncInserted")]
        RestResponse<int> syncInserted([Body] RolLocal rolLocal);
        [Post("roles/syncUpdated")]
        RestResponse<int> syncUpdated([Body] RolLocal rolLocal);
    }
}
