using DataAccess.Local;
using DataAccess.Server;
using IsecService.Models;
using IsecService.Server.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
namespace IsecService.Server.Repositorios
{
    public class ModuloRepository : IModuloRepository
    {
        private string connection = Connection.Instance.ConnectionString();

        public int AddModuloInserted(ModuloLocal moduloLocal)
        {
            throw new NotImplementedException();
        }

        public int AddModuloUpdated(ModuloLocal moduloLocal)
        {
            throw new NotImplementedException();
        }

        public List<ModuloServer> GetAll()
        {
            List<ModuloServer> modulos = new List<ModuloServer>();
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_GetAllModulos",conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ModuloServer modulo = new ModuloServer();
                        modulo.Id = (int)rd["id"];
                        modulo.Nombre = rd["nombre"] as string;
                        modulos.Add(modulo);
                    }
                }
                return modulos; 
            }
        }

        public List<int> ModulosNoSync(List<ModuloLocal> modulosLocal)
        {
            throw new NotImplementedException();
        }
    }
}