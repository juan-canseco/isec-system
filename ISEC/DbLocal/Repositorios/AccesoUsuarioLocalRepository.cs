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
    public class AccesoUsuarioLocalRepository : IAccesoUsuarioLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;
        ModuloLocalRepository repoModulo = new ModuloLocalRepository();
        RolLocalRepository repoRol = new RolLocalRepository();
        public bool Add(AccesoUsuarioLocal accesoUsuarioLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd= new SQLiteCommand("insert into accesousuario (rolid,moduloid,acceso,lectura,escritura,sync) VALUES(@rol,@modulo,@a,@l,@e,@sync)",conn))
                {
                    cmd.Parameters.AddWithValue("@rol", accesoUsuarioLocal.RolId);
                    cmd.Parameters.AddWithValue("@modulo", accesoUsuarioLocal.ModuloId);
                    cmd.Parameters.AddWithValue("@a", accesoUsuarioLocal.Acceso);
                    cmd.Parameters.AddWithValue("@l", accesoUsuarioLocal.Lectura);
                    cmd.Parameters.AddWithValue("@e", accesoUsuarioLocal.Escritura); 
                    cmd.Parameters.AddWithValue("@sync", 0); 
                    var rd = cmd.ExecuteNonQuery();
                    inserted = rd > 0 ? true : false;
                }
            }
            return inserted;
        }

        public int countByRolId(int id)
        {
            int count = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select count(*) from accesousuario where rolid=@id", conn))
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

        public List<AccesoUsuarioLocal> ConvertPermisosToLocal(List<AccesoUsuarioServer> permisos)
        {
            List<AccesoUsuarioLocal> permisosLocales = new List<AccesoUsuarioLocal>();
            if (permisos.Count>0)
            {
                foreach (var p in permisos)
                {
                    var pp = new AccesoUsuarioLocal();
                    pp.Id = p.Id;
                    pp.RolId = p.RolId;
                    pp.ModuloId = p.ModuloId;
                    pp.Acceso = p.Acceso;
                    pp.Lectura = p.Lectura;
                    pp.Escritura = p.Escritura;
                    pp.Sync = p.Sync;
                    permisosLocales.Add(pp);
                } 
            }
            return permisosLocales;
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }

        public AccesoUsuarioLocal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<AccesoUsuarioLocal> GetAll()
        {
            List<AccesoUsuarioLocal> accesos = new List<AccesoUsuarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from accesousuario order by id desc",conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        AccesoUsuarioLocal acc = new AccesoUsuarioLocal();
                        acc.Id = int.Parse(rd["id"].ToString());
                        acc.RolId = int.Parse(rd["rolid"].ToString());
                        acc.Rol = repoRol.Get(acc.RolId).Descripcion;
                        acc.ModuloId = int.Parse(rd["moduloid"].ToString());
                        acc.Modulo = repoModulo.Get(acc.ModuloId).Nombre;
                        acc.Acceso = int.Parse(rd["acceso"].ToString()) >0 ? true:false;
                        acc.Lectura = int.Parse(rd["lectura"].ToString()) > 0 ? true : false;
                        acc.Escritura = int.Parse(rd["escritura"].ToString()) > 0 ? true : false ;
                        acc.Sync = int.Parse(rd["sync"].ToString());
                        accesos.Add(acc); 
                    }
                }
                return accesos;
            }
        }

        public List<AccesoUsuarioLocal> GetAllByRol(int rol)
        {
            List<AccesoUsuarioLocal> accesos = new List<AccesoUsuarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from accesousuario where rolid=@rol", conn))
                {
                    cmd.Parameters.AddWithValue("@rol", rol);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        AccesoUsuarioLocal acc = new AccesoUsuarioLocal();
                        acc.Id = int.Parse(rd["id"].ToString());
                        acc.RolId = int.Parse(rd["rolid"].ToString());
                        acc.Rol = repoRol.Get(acc.RolId).Descripcion;
                        acc.ModuloId = int.Parse(rd["moduloid"].ToString());
                        acc.Modulo = repoModulo.Get(acc.ModuloId).Nombre;
                        acc.Acceso = int.Parse(rd["acceso"].ToString()) > 0 ? true : false;
                        acc.Lectura = int.Parse(rd["lectura"].ToString()) > 0 ? true : false;
                        acc.Escritura = int.Parse(rd["escritura"].ToString()) > 0 ? true : false;
                        acc.Sync = int.Parse(rd["sync"].ToString());
                        accesos.Add(acc);
                    }
                }
                return accesos;
            }
        }

        public bool Update(AccesoUsuarioLocal accesoUsuarioLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update accesousuario SET rolid=@rol,moduloid=@modulo,acceso=@a,lectura=@l,escritura=@e WHERE id=@id ", conn))
                {
                    cmd.Parameters.AddWithValue("@id", accesoUsuarioLocal.Id);
                    cmd.Parameters.AddWithValue("@rol", accesoUsuarioLocal.RolId);
                    cmd.Parameters.AddWithValue("@modulo", accesoUsuarioLocal.ModuloId);
                    cmd.Parameters.AddWithValue("@a", accesoUsuarioLocal.Acceso);
                    cmd.Parameters.AddWithValue("@l", accesoUsuarioLocal.Lectura);
                    cmd.Parameters.AddWithValue("@e", accesoUsuarioLocal.Escritura);
                    var rd = cmd.ExecuteNonQuery();
                    updated = rd > 0 ? true : false;
                }
            }
            return updated;
        }

        public void UpdateSync(List<int> data)
        {
            if (data.Count > 0)
            {
                foreach (var id in data)
                {
                    using (var conn = new SQLiteConnection(connection))
                    {
                        conn.Open();
                        using (var cmd = new SQLiteCommand($"update accesousuario set sync=1 WHERE id=@id", conn))
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
