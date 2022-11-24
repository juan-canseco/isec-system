using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using DataAccess.Server;

namespace ISEC.DbLocal.Repositorios
{
    public class RolUsuarioLocalRepository : IRolUsuarioRepository
    {
        string connection = ConnectionLocal.ConecctionString;
        RolLocalRepository rolRepo = new RolLocalRepository();
        UsuarioLocalRepository userRepo = new UsuarioLocalRepository();
        public bool Add(RolUsuarioLocal rolUsuarioLocal)
        {
            bool inserted = false;
            using (var conn = new  SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into rolusuario (rolid,usuarioid,sync) VALUES (@r,@us,@sync)",conn))
                {
                    cmd.Parameters.AddWithValue("@r", rolUsuarioLocal.RolId);
                    cmd.Parameters.AddWithValue("@us", rolUsuarioLocal.UsuarioId);
                    cmd.Parameters.AddWithValue("@sync", 0);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }
        public List<RolUsuarioLocal> ConvertRolUsuariosToLocal(List<RolUsuarioServer> rolesUsuariosServer)
        {
            List<RolUsuarioLocal> rolusuariosLocales = new List<RolUsuarioLocal>();
            if (rolesUsuariosServer.Count > 0)
            {
                foreach (var role in rolesUsuariosServer)
                {
                    RolUsuarioLocal rolusuarioLocal = new RolUsuarioLocal();
                    rolusuarioLocal.Id = role.Id;
                    rolusuarioLocal.RolId = role.RolId;
                    rolusuarioLocal.UsuarioId = role.UsuarioId;
                    rolusuariosLocales.Add(rolusuarioLocal);
                }
            }
            return rolusuariosLocales;
        }
        public int CountByUser(int id)
        {
            int count = 0; 
            using (var conn= new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select count(*) from rolusuario where usuarioid=@id",conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        count = int.Parse(rd[0].ToString());
                    }
                } 
            }
            return count;
        }

        public bool Delete(int id)
        {
            bool deleted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("delete from rolusuario where id=@id",conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var result = cmd.ExecuteNonQuery();
                    deleted = result > 0 ? true : false;
                }
                return deleted;
            }
        }

        public bool Exists(int rolid, int usuarioid)
        {
            bool exists = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select id from rolusuario where rolid=@r AND usuarioid=@us",conn))
                {
                    cmd.Parameters.AddWithValue("@r", rolid);
                    cmd.Parameters.AddWithValue("@us", usuarioid);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        var result = int.Parse(rd[0].ToString());
                        exists = result>0 ? true :false;
                    }

                }
                return exists;
            }
        }

        public List<RolUsuarioLocal> Get(int userid)
        {
            List<RolUsuarioLocal> roles = new List<RolUsuarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rolusuario  where usuarioid=@r", conn))
                {
                    cmd.Parameters.AddWithValue("@r", userid);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolUsuarioLocal rol = new RolUsuarioLocal();
                        rol.Id = int.Parse(rd["id"].ToString());
                        rol.RolId = int.Parse(rd["rolid"].ToString());
                        rol.UsuarioId = int.Parse(rd["usuarioid"].ToString());
                        roles.Add(rol);
                    }
                }
                return roles;
            }
        }

        public List<RolUsuarioLocal> GetAll()
        {
            List<RolUsuarioLocal> roles = new List<RolUsuarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rolusuario order by id desc", conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolUsuarioLocal rol = new RolUsuarioLocal();
                        rol.Id = int.Parse(rd["id"].ToString());
                        rol.RolId = int.Parse(rd["rolid"].ToString());
                        rol.Rol = rolRepo.Get(rol.RolId).Descripcion;
                        rol.UsuarioId = int.Parse(rd["usuarioid"].ToString());
                        rol.Usuario = userRepo.Get(rol.UsuarioId).Nombre;
                        rol.Sync = int.Parse(rd["sync"].ToString());
                        roles.Add(rol);
                    }
                }
                return roles;
            }
        }

        public List<RolUsuarioLocal> GetAllByRolId(int id)
        {
            List<RolUsuarioLocal> roles = new List<RolUsuarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rolusuario where rolid=@id or usuarioid=@id order by id desc", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolUsuarioLocal rol = new RolUsuarioLocal();
                        rol.Id = int.Parse(rd["id"].ToString());
                        rol.RolId = int.Parse(rd["rolid"].ToString());
                        rol.Rol = rolRepo.Get(rol.RolId).Descripcion;
                        rol.UsuarioId = int.Parse(rd["usuarioid"].ToString());
                        rol.Usuario = userRepo.Get(rol.UsuarioId).Nombre;
                        rol.Sync = int.Parse(rd["sync"].ToString());
                        roles.Add(rol);
                    }
                }
                return roles;
            }
        }

        public bool Update(RolUsuarioLocal rolUsuarioLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update rolusuario set rolid=@r,usuarioid=@us,sync=@sync where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", rolUsuarioLocal.Id);
                    cmd.Parameters.AddWithValue("@r", rolUsuarioLocal.RolId);
                    cmd.Parameters.AddWithValue("@us", rolUsuarioLocal.UsuarioId);
                    cmd.Parameters.AddWithValue("@sync", 1);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }

        public void UpdateSync(List<int> rolUsuarios)
        {

            if (rolUsuarios.Count > 0)
            {
                foreach (var id in rolUsuarios)
                {
                    using (var conn = new SQLiteConnection(connection))
                    {
                        conn.Open();
                        using (var cmd = new SQLiteCommand($"update rolusuario set sync=1 WHERE id=@id", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@id", id);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
