using IsecService.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using DataAccess.Local;
namespace IsecService.Server.Interfaces
{
    public interface IUsuarioRepository
    {
        List<Usuario> GetAll();
        List<Usuario> GetAllNoSync();
        int GetLastFolio();
        int Add(Usuario usuario);
        int Update(Usuario usuario);
        Usuario Login(string username, string password);
        List<int> UsersNoSync(List<UsuarioLocal> usuariosLocales);
        void AddUserInserted(UsuarioLocal usuarioLocal);
        void AddUserUpdated(UsuarioLocal usuarioLocal);
    }
}
