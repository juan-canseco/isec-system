using IsecService.Models;
using IsecService.Server.Interfaces;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using DataAccess;
using DataAccess.Local;
using DataAccess.Server;

namespace IsecService.Server.Repositorios
{
    public class RolRepository : IRolRepository
    {
        private Connection connection = Connection.Instance;

        public int Add(RolServer rol)
        {
            int inserted = 0; 
            if (!Exists(rol.Descripcion))
            {
                using (var conn = new MySqlConnection(connection.ConnectionString()))
                {
                    conn.Open();
                    using (var cmd = new MySqlCommand("RolAdd", conn))
                    {
                        var putoid = Last() + 1;
                        cmd.Parameters.AddWithValue("@idRol", putoid);
                        cmd.Parameters.AddWithValue("@descripcion", rol.Descripcion);
                        cmd.Parameters.AddWithValue("@sync", 0);
                        cmd.Parameters.AddWithValue("@activo", 1);
                        cmd.CommandType = CommandType.StoredProcedure;
                        var rd = cmd.ExecuteNonQuery();
                        inserted = rd > 0 || rd != -1 ? rd : 0;
                    }
                }
            }
            else
            {
                inserted = 2;
            } 
            return inserted;
        }

        public int AddRolInserted(RolLocal rolLocal)
        {
            int synced = 0;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("RolAdd", conn))
                {
                    cmd.Parameters.AddWithValue("@idRol", rolLocal.Id);
                    cmd.Parameters.AddWithValue("@descripcion", rolLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@sync", 1);
                    cmd.Parameters.AddWithValue("@activo", rolLocal.Activo);  
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery();
                    synced = rd;
                }
            }
            return synced;
        } 
        public int AddRolUpdated(RolLocal rolLocal)
        {
            int synced = 0; 
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("RolUpdate", conn))
                {
                    cmd.Parameters.AddWithValue("@id", rolLocal.Id);
                    cmd.Parameters.AddWithValue("@descripcion", rolLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@sync", 1);
                    cmd.Parameters.AddWithValue("@activo", rolLocal.Activo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery();
                    synced = rd;
                }
            }
            return synced;
        }

        public bool Exists(string rol)
        {
            bool exists = false;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_ExistsRol", conn))
                {
                    cmd.Parameters.AddWithValue("@rol", rol);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        exists = (int)rd[0] > 0 ? true : false;
                    }
                }
            }
            return exists;
        }

        public List<RolServer> GetAll()
        {
            List<RolServer> roles = new List<RolServer>();
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("GetAllRoles", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolServer rol = new RolServer();
                        rol.Id = (int)rd["id"];
                        rol.Descripcion = (string)rd["descripcion"];
                        rol.Sync =int.Parse(rd["sync"].ToString());
                        rol.Activo =int.Parse( rd["activo"].ToString());
                        rol.LastUpdate =rd["lastupdate"] != DBNull.Value? (DateTime?)rd["lastupdate"]:null; 
                        roles.Add(rol);
                    }
                }
                return roles;
            }
        }

        public int Last()
        {
            int last = 0;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_GetLastRol", conn))
                { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = (int)rd["id"];
                    }
                }
            }
            return last;
        }

        public List<int> RolesNoSync(List<RolLocal> rolesLocales)
        {
            List<int> nosync = new List<int>();

            if (rolesLocales.Count > 0)
            {

                if (GetAll().Count > 0)
                {
                    foreach (var rolLocal in rolesLocales)
                    {
                        if (GetAll().Where(s => s.Id == rolLocal.Id).ToList().Count > 0)
                        {
                            using (var conn = new MySqlConnection(connection.ConnectionString()))
                            {
                                conn.Open();
                                using (var cmd = new MySqlCommand("RolNoSync", conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", rolLocal.Id);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    var rd = cmd.ExecuteNonQuery();
                                    if (rd > 0)
                                    {
                                        nosync.Add(rolLocal.Id);
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var conn = new MySqlConnection(connection.ConnectionString()))
                            {
                                conn.Open();
                                using (var cmd = new MySqlCommand("RolAdd", conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", rolLocal.Id);
                                    cmd.Parameters.AddWithValue("@descripcion", rolLocal.Descripcion);
                                    cmd.Parameters.AddWithValue("@sync", 1);
                                    cmd.Parameters.AddWithValue("@activo", rolLocal.Activo);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    var rd = cmd.ExecuteNonQuery();
                                    if (rd > 0)
                                    {
                                        nosync.Add(rolLocal.Id);
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    foreach (var rolLocal in rolesLocales)
                    {
                        using (var conn = new MySqlConnection(connection.ConnectionString()))
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("RolAdd", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", rolLocal.Id);
                                cmd.Parameters.AddWithValue("@descripcion", rolLocal.Descripcion);
                                cmd.Parameters.AddWithValue("@sync", 1);
                                cmd.Parameters.AddWithValue("@activo", rolLocal.Activo);
                                cmd.CommandType = CommandType.StoredProcedure;
                                var rd = cmd.ExecuteNonQuery();
                                if (rd > 0)
                                {
                                    nosync.Add(rolLocal.Id);
                                }
                            }
                        }
                    }
                }
            }
            return nosync;
        }
    }
}