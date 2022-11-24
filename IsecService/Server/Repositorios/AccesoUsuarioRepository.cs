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
    public class AccesoUsuarioRepository : IAccesoUsuarioRepository
    {
        private string connection = Connection.Instance.ConnectionString();
        private ModuloRepository moduloRepo = new ModuloRepository();

        public List<AccesoUsuarioServer> GetAll()
        {
            List<AccesoUsuarioServer> permisos = new List<AccesoUsuarioServer>();
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_GetPermisos", conn))
                { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var acc = new AccesoUsuarioServer();
                        acc.Id = (int)rd["id"];
                        acc.RolId = (int)rd["rolid"];
                        acc.ModuloId = (int)rd["moduloid"];
                        acc.Acceso = (int)rd["acceso"] > 0 ? true : false;
                        acc.Lectura = (int)rd["lectura"] > 0 ? true : false;
                        acc.Escritura = (int)rd["escritura"] > 0 ? true : false;
                        acc.Modulo = moduloRepo.GetAll().Where(s => s.Id == acc.ModuloId).FirstOrDefault().Nombre;
                        acc.Sync = (int)rd["sync"];
                        permisos.Add(acc);
                    }
                }
            }
            return permisos;
        }

        public List<AccesoUsuarioServer> GetAllByRol(int rol)
        {
            List<AccesoUsuarioServer> accesos = new List<AccesoUsuarioServer>();
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_GetAccesosByRol", conn))
                { 
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var acc = new AccesoUsuarioServer();
                        acc.Id = (int)rd["id"];
                        acc.RolId = (int)rd["rolid"];
                        acc.ModuloId = (int)rd["moduloid"];
                        acc.Acceso = (int)rd["acceso"] > 0 ? true : false;
                        acc.Lectura = (int)rd["lectura"] > 0 ? true : false;
                        acc.Escritura = (int)rd["escritura"] > 0 ? true : false;
                        acc.Modulo = moduloRepo.GetAll().Where(s => s.Id == acc.ModuloId).FirstOrDefault().Nombre;
                        acc.Sync = (int)rd["sync"];
                        accesos.Add(acc);
                    }
                }
            }
            return accesos;
        }

        public void Updated(AccesoUsuarioServer permiso)
        {
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_UpdatePermiso",conn))
                {
                    cmd.Parameters.AddWithValue("@id", permiso.Id);
                    cmd.Parameters.AddWithValue("@acc", permiso.Acceso);
                    cmd.Parameters.AddWithValue("@l", permiso.Lectura);
                    cmd.Parameters.AddWithValue("@e", permiso.Escritura);
                    cmd.Parameters.AddWithValue("@rol", permiso.RolId);
                    cmd.Parameters.AddWithValue("@modulo", permiso.ModuloId);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var result=cmd.ExecuteNonQuery();
                }
            }
        }
    }
}