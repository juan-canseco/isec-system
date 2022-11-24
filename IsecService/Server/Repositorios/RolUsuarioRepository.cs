using DataAccess.Server;
using IsecService.Models;
using IsecService.Server.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IsecService.Server.Repositorios
{
    public class RolUsuarioRepository : IRolUsuarioRepository
    {
        private string connection = Connection.Instance.ConnectionString(); 
        public int Add(RolUsuarioServer rolUsuarioServer)
        {
            int inserted = 0;
            if (existe(rolUsuarioServer.RolId,rolUsuarioServer.UsuarioId) == 0)
            {
                using (var conn = new MySqlConnection(connection))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("sp_AddUserRol", conn))
                    {
                        cmd.Parameters.AddWithValue("@id", rolUsuarioServer.Id);
                        cmd.Parameters.AddWithValue("@rol", rolUsuarioServer.RolId);
                        cmd.Parameters.AddWithValue("@userid", rolUsuarioServer.UsuarioId);
                        cmd.CommandType = System.Data.CommandType.StoredProcedure;
                        var result = cmd.ExecuteNonQuery();
                        inserted = result;
                    }
                }
            }
            else
            {
                inserted = 2;
            } 
            return inserted;
        }

        public int existe(int rol, int user)
        {
            int inserted = 0;
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_ExisteRolUsuario", conn))
                {
                    cmd.Parameters.AddWithValue("@rol",rol);
                    cmd.Parameters.AddWithValue("@userid",user);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteReader();
                    if (result.Read())
                    {
                        inserted = (int)result[0];
                    }
                }
            }
            return inserted;
        }

        public List<RolUsuarioServer> GetAllByRol(int id)
        {
            List<RolUsuarioServer> roles = new List<RolUsuarioServer>();
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_UserRoles", conn))
                {
                    cmd.Parameters.AddWithValue("@rol", id); 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteReader();
                    while (result.Read())
                    {
                        RolUsuarioServer rol = new RolUsuarioServer();
                        rol.Id = (int)result["id"];
                        rol.RolId = (int)result["rolid"];
                        rol.UsuarioId = (int)result["usuarioid"];
                        rol.Sync = (int)result["sync"];
                        roles.Add(rol);
                    }
                }
            } 
            return roles;
        }

        public int Last()
        {
            int last = 0;
            using (var conn = new MySqlConnection(connection))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_LastRolUsuarioId", conn))
                { 
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var result = cmd.ExecuteReader();
                    if (result.Read())
                    {
                        last = (int)result[0];
                    }
                }
            }
            return last;
        }
    }
}