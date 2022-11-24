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

namespace IsecService.Server.Repositorios
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private Connection connection = Connection.Instance;

        public List<Usuario> GetAll()
        {
            List<Usuario> usuarios = new List<Usuario>();
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("GetAllUsers", conn))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.Id = (int)rd["id"];
                        usuario.Nombre = (string)rd["nombre"];
                        usuario.Username = rd["username"] as string;
                        usuario.Password = rd["password"] as string;
                        usuario.puestoid = (int)rd["puestoid"];
                        usuario.Puesto = rd["puesto"] as string;
                        usuario.Activo = (int)rd["activo"];
                        usuario.Sync = (int)rd["sync"]; 
                        usuario.LastUpdate = rd["lastupdate"] != DBNull.Value ? (DateTime?)rd["lastupdate"] :null;
                        usuario.Rol = rd["rol"] as string;
                        usuarios.Add(usuario);
                    }
                }
                return usuarios;
            }
        }
         

        public void AddUserInserted(UsuarioLocal usuarioLocal)
        {
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("UserAdd", conn))
                {
                    cmd.Parameters.AddWithValue("@id", usuarioLocal.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuarioLocal.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuarioLocal.Username);
                    cmd.Parameters.AddWithValue("@ppassword", usuarioLocal.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuarioLocal.puestoid);
                    cmd.Parameters.AddWithValue("@sync", 1);
                    cmd.Parameters.AddWithValue("@activo", usuarioLocal.Activo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery(); 
                }
            }
        }
        public void AddUserUpdated(UsuarioLocal usuarioLocal)
        {
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("UserUpdate", conn))
                {
                    cmd.Parameters.AddWithValue("@id", usuarioLocal.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuarioLocal.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuarioLocal.Username);
                    cmd.Parameters.AddWithValue("@ppassword", usuarioLocal.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuarioLocal.puestoid);
                    cmd.Parameters.AddWithValue("@sync", 1);
                    cmd.Parameters.AddWithValue("@activo", usuarioLocal.Activo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery(); 
                }
            }
        }

        public List<int> UsersNoSync(List<UsuarioLocal> usuariosLocales)
        {
            List<int> nosync = new List<int>();

            if (usuariosLocales.Count > 0)
            {

                if (GetAll().Count > 0)
                {
                    foreach (var us in usuariosLocales)
                    {
                        if (GetAll().Where(s => s.Id == us.Id).ToList().Count > 0)
                        {
                            using (var conn = new MySqlConnection(connection.ConnectionString()))
                            {
                                conn.Open();
                                using (var cmd = new MySqlCommand("UserNoSync", conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", us.Id);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    var rd = cmd.ExecuteNonQuery();
                                    if (rd > 0)
                                    {
                                        nosync.Add(us.Id);
                                    }
                                }
                            }
                        }
                        else
                        {
                            using (var conn = new MySqlConnection(connection.ConnectionString()))
                            {
                                conn.Open();
                                using (var cmd = new MySqlCommand("UserAdd", conn))
                                {
                                    cmd.Parameters.AddWithValue("@id", us.Id);
                                    cmd.Parameters.AddWithValue("@nombre", us.Nombre);
                                    cmd.Parameters.AddWithValue("@username", us.Username);
                                    cmd.Parameters.AddWithValue("@ppassword", us.Password);
                                    cmd.Parameters.AddWithValue("@puestoid", us.puestoid);
                                    cmd.Parameters.AddWithValue("@sync", 1);
                                    cmd.Parameters.AddWithValue("@activo", us.Activo);
                                    cmd.CommandType = CommandType.StoredProcedure;
                                    var rd = cmd.ExecuteNonQuery();
                                    if (rd > 0)
                                    {
                                        nosync.Add(us.Id);
                                    }
                                }
                            }
                        }

                    }
                }
                else
                {
                    foreach (var us in usuariosLocales)
                    {
                        using (var conn = new MySqlConnection(connection.ConnectionString()))
                        {
                            conn.Open();
                            using (var cmd = new MySqlCommand("UserAdd", conn))
                            {
                                cmd.Parameters.AddWithValue("@id", us.Id);
                                cmd.Parameters.AddWithValue("@nombre", us.Nombre);
                                cmd.Parameters.AddWithValue("@username", us.Username);
                                cmd.Parameters.AddWithValue("@ppassword", us.Password);
                                cmd.Parameters.AddWithValue("@puestoid", us.puestoid);
                                cmd.Parameters.AddWithValue("@sync", 1);
                                cmd.Parameters.AddWithValue("@activo", us.Activo);
                                cmd.CommandType = CommandType.StoredProcedure;
                                var rd = cmd.ExecuteNonQuery();
                                if (rd > 0)
                                {
                                    nosync.Add(us.Id);
                                }
                            }
                        }
                    } 
                } 
            } 
            return nosync;
        }

        public Usuario Login(string username, string password)
        {
            Usuario usuario = null;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_Login", conn))
                { 
                    cmd.Parameters.AddWithValue("@username", username);
                    cmd.Parameters.AddWithValue("@ppassword", password); 
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        usuario = new Usuario();
                        usuario.Id = (int)rd["id"];
                        usuario.Nombre = (string)rd["nombre"];
                        usuario.Username = rd["username"] as string;
                        usuario.Password = rd["password"] as string;
                        usuario.puestoid = (int)rd["puestoid"];
                        usuario.Activo = (int)rd["activo"];
                        usuario.Sync = (int)rd["sync"];
                        usuario.LastUpdate = (DateTime)rd["lastupdate"];
                    }
                }
            }
            return usuario;
        }

        public int GetLastFolio()
        {
            int last = 0;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("sp_LastFolio", conn))
                { 
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = int.Parse(rd[0].ToString());
                    }
                }
            }
            return last;
        }

        public int Add(Usuario usuario)
        {
            int result = 0;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("UserAdd", conn))
                {
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuario.Username);
                    cmd.Parameters.AddWithValue("@ppassword", usuario.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuario.puestoid);
                    cmd.Parameters.AddWithValue("@sync", 0);
                    cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery();
                    if (rd > 0 && rd != -1)
                    {
                        result = rd; 
                    }
                }
                return result;
            }
        }

        public int Update(Usuario usuario)
        {
            int result = 0;
            using (var conn = new MySqlConnection(connection.ConnectionString()))
            {
                conn.Open();
                using (var cmd = new MySqlCommand("UserAdd", conn))
                {
                    cmd.Parameters.AddWithValue("@id", usuario.Id);
                    cmd.Parameters.AddWithValue("@nombre", usuario.Nombre);
                    cmd.Parameters.AddWithValue("@username", usuario.Username);
                    cmd.Parameters.AddWithValue("@ppassword", usuario.Password);
                    cmd.Parameters.AddWithValue("@puestoid", usuario.puestoid);
                    cmd.Parameters.AddWithValue("@sync", usuario.Sync);
                    cmd.Parameters.AddWithValue("@activo", usuario.Activo);
                    cmd.CommandType = CommandType.StoredProcedure;
                    var rd = cmd.ExecuteNonQuery();
                    if (rd > 0 && rd != -1)
                    {
                        result = rd;
                    }
                }
                return result;
            }
        }

        public List<Usuario> GetAllNoSync()
        {
            throw new NotImplementedException();
        }
    }
}