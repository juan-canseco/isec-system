using DataAccess;
using DataAccess.Local;
using DataAccess.Server;
using RestSharp;
using Retrofit.Net.Attributes.Methods;
using Retrofit.Net.Attributes.Parameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SyncData.API.Users
{
    public interface IUserService
    {
        [Get("usuarios/all/")]
        RestResponse<List<Usuario>> All();

        [Post("usuarios/sync/")]
        RestResponse<List<int>> sync([Body] List<UsuarioLocal> usuariosLocales);

        [Post("usuarios/syncInserted")]
        RestResponse<int> syncInserted([Body] UsuarioLocal usuarioLocal);
        [Post("usuarios/syncUpdated")]
        RestResponse<int> syncUpdated([Body] UsuarioLocal usuarioLocal);
    }
}
