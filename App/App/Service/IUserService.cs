using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;
using DataAccess;
namespace App.Service
{
    public interface IUserService
    { 
        [Get("/usuarios/Login?username={username}&password={password}")] 
        Task<Usuario> Login(string username,string password);
        [Get("/usuarios/all")]
        Task<List<Usuario>> all();
    }
}
