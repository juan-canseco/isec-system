using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks; 

namespace ISEC.DbLocal.Interfaces
{
    public  interface IUsuarioLocalRepository
    {
        List<UsuarioLocal> GetAll();
        List<UsuarioLocal> GetAllNoSync();
        UsuarioLocal GetLast();
        UsuarioLocal Get(int id);
        void UpdateSync(List<int> id);
        UsuarioLocal Login (string username,string password);
        UsuarioLocal LoginByPassword (string password); 
        bool Add(UsuarioLocal usuarioLocal);
        bool Update(UsuarioLocal usuarioLocal);
        bool Desactivate(int id); 


    }
}
