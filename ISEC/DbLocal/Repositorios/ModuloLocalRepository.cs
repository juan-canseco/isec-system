using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System.Data.SQLite;
namespace ISEC.DbLocal.Repositorios
{
    public class ModuloLocalRepository : IModuloLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;

        public bool Add(ModuloLocal moduloLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into modulo (nombre) values(@mod)", conn))
                {
                    cmd.Parameters.AddWithValue("@mod", moduloLocal.Nombre);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }

            }
            return inserted;
        }

        public bool Exists(string nombre)
        {
            bool inserted = false;
            int count = 0;
            try
            {

                using (var conn = new SQLiteConnection(connection))
                {
                    conn.Open();

                    using (var cmd = new SQLiteCommand("SELECT count(*) FROM modulo WHERE nombre=@nombre", conn))
                    {
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        var result = cmd.ExecuteReader();
                        if (result.Read())
                        {
                            count = int.Parse(result[0].ToString());
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

        public ModuloLocal Get(int id)
        {
            ModuloLocal moduloLocal = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from modulo where id=@id",conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        moduloLocal = new ModuloLocal();
                        moduloLocal.Id = int.Parse(rd["id"].ToString());
                        moduloLocal.Nombre = rd["nombre"] as string;
                    }
                }
            }
            return moduloLocal;
        }

        public ModuloLocal Get(ModuloLocal id)
        {
            throw new NotImplementedException();
        }

        public List<ModuloLocal> GetAll()
        {
            List<ModuloLocal> modulos = new List<ModuloLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd  = new SQLiteCommand("select * from modulo",conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var m = new ModuloLocal();
                        m.Id = int.Parse(rd["id"].ToString());
                        m.Nombre =rd["nombre"] as string;
                        modulos.Add(m);
                    }
                } 
            }
            return modulos;


        }

        public bool Update(ModuloLocal moduloLocal)
        {
            throw new NotImplementedException();
        }
    }
}
