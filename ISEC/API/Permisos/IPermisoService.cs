using DataAccess.Server;
using RestSharp;
using Retrofit.Net.Attributes.Methods; 
using System.Collections.Generic; 

namespace ISEC.API.Permisos
{
    public interface IPermisoService
    {
        [Get("permisos/all/")]
        RestResponse<List<AccesoUsuarioServer>> all();
    }
}
