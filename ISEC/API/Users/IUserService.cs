using RestSharp;
using Retrofit.Net.Attributes.Methods;
using System;
using System.Collections.Generic; 
using DataAccess;
using Retrofit.Net.Attributes.Parameters;
using DataAccess.Local;
namespace ISEC.API.Users
{
    public interface IUserService
    {
        [Get("usuarios/all/")]
        RestResponse<List<Usuario>> All();

        [Post("usuarios/sync/")]
        RestResponse<List<int>> sync([Body]List<UsuarioLocal> usuariosLocales);

        [Post("usuarios/syncInserted")]
        RestResponse<int> syncInserted([Body] UsuarioLocal usuarioLocal);
        [Post("usuarios/syncUpdated")]
        RestResponse<int> syncUpdated([Body] UsuarioLocal usuarioLocal); 

        //[Get("people/{id}")]
        //RestResponse<Person> GetPerson([Path("id")] int id, [Query("limit")] int limit, [Query("test")] string test); 
    }
}
