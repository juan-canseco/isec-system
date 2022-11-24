using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using DataAccess.Server;
using Microsoft.Data.Sqlite;

namespace ISEC.DbLocal.Repositorios
{
    public class RolLocalRepository : IRolLocalRepository
    {
        private string connection = ConnectionLocal.ConecctionString;
        public bool Add(RolLocal rolLocal)
        {
            bool inserted = false;

            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand("insert into rol(descripcion,sync) values (@desc,@sync)", conn))
                {
                    cmd.Parameters.AddWithValue("@desc", rolLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@sync", 0);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }

        public List<RolLocal>  ConvertServerRolToLocal(List<RolServer> rolesServer)
        {
            List<RolLocal> rolLocales = new List<RolLocal>();
            foreach (var rol in rolesServer)
            {
                RolLocal rolS = new RolLocal();
                rolS.Id = rol.Id;
                rolS.Descripcion = rol.Descripcion;
                rolS.Sync = rol.Sync;
                rolS.Activo = rol.Activo;
                rolLocales.Add(rolS);
            }
            return rolLocales;


        }
        public bool Desactivate(int id)
        {
            throw new NotImplementedException();
        }

        public bool Exists(string rol)
        {
            bool inserted = false;
            int count = 0;
            try
            { 
                using (var conn = new SQLiteConnection(connection))
                {
                    conn.Open(); 
                    using (var cmd = new SQLiteCommand("SELECT id FROM rol WHERE descripcion=@rol",conn))
                    {
                        cmd.Parameters.AddWithValue("@rol", rol);
                        var result = cmd.ExecuteReader();
                        if (result.Read())
                        {
                            count = (int)result[0];
                            inserted = count > 0 ? true : false;
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                string n = ex.Message;
            }
      
            return inserted;
        }

        public RolLocal Get(int id)
        {
            RolLocal rol = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rol where id=@id",conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        rol = new RolLocal();
                        rol.Id = int.Parse(rd["id"].ToString());
                        rol.Descripcion = rd["descripcion"] as string;
                        rol.Sync = int.Parse(rd["sync"].ToString());
                        rol.Activo = int.Parse(rd["activo"].ToString());
                    }
                }
                return rol;
            }
        }

        public List<RolLocal> GetAll()
        {
            List<RolLocal> roles = new List<RolLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rol order by id", conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolLocal rol = new RolLocal();
                        rol.Id =!string.IsNullOrEmpty( rd["id"].ToString())?  int.Parse(rd["id"].ToString()) : 0;
                        rol.Descripcion = rd["descripcion"] as string;
                        rol.Sync = int.Parse(rd["sync"].ToString());
                        rol.Activo = int.Parse(rd["activo"].ToString());
                        roles.Add(rol);
                    }

                }
            }
            return roles;
        }

        public List<RolLocal> GetAllNoSync()
        {
            List<RolLocal> roles = new List<RolLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from rol where sync=0", conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        RolLocal rol = new RolLocal();
                        rol.Id = !string.IsNullOrEmpty(rd["id"].ToString()) ? int.Parse(rd["id"].ToString()) : 0;
                        rol.Descripcion = rd["descripcion"] as string;
                        rol.Sync = int.Parse(rd["sync"].ToString());
                        rol.Activo = int.Parse(rd["activo"].ToString());
                        roles.Add(rol);
                    }

                }
            }
            return roles;
        }

        public int GetLast()
        {
            int last = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM rol ORDER BY id DESC LIMIT 1", conn))
                {
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = int.Parse(rd[0].ToString());
                    }
                }
            }
            return last;
        }

        public bool Update(RolLocal rolLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();

                using (var cmd = new SQLiteCommand("update rol set descripcion=@desc,sync=@sync,activo=@ac WHERE id=@id", conn))
                {
                    cmd.Parameters.Add("@id", DbType.Int32).Value = rolLocal.Id; 
                    cmd.Parameters.Add("@desc", DbType.String).Value = rolLocal.Descripcion;
                    cmd.Parameters.Add("@sync", DbType.Int32).Value = rolLocal.Sync;
                    cmd.Parameters.Add("@ac", DbType.Int32).Value = rolLocal.Activo;
                    cmd.CommandType = System.Data.CommandType.Text;
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                } 
            }
            return updated;
        }

        public void UpdateSync(List<int> roles)
        {
            if (roles !=null && roles.Count > 0)
            {
                foreach (var rolid in roles)
                {
                    using (var conn = new SQLiteConnection(connection))
                    {
                        conn.Open();
                        using (var cmd = new SQLiteCommand($"update rol set sync=1 WHERE id=@id", conn))
                        {
                            cmd.CommandType = System.Data.CommandType.Text;
                            cmd.Parameters.AddWithValue("@id", rolid);
                            cmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
    }
}
