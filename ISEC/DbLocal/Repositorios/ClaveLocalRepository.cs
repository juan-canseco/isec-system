using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
namespace ISEC.DbLocal.Repositorios
{
    public class ClaveLocalRepository : IClaveLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;

        public bool Add(ClaveLocal claveLocal)
        {
            bool inserted = false;

            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into clave (clave,cuota,precio) values(@clave,@cuota,@precio)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@clave", claveLocal.Clave);
                    cmd.Parameters.AddWithValue("@cuota", claveLocal.Cuota);
                    cmd.Parameters.AddWithValue("@precio", claveLocal.Precio);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                    
                }
            }
            return inserted;
        }

        public bool Desactivate(int id)
        {
            throw new NotImplementedException();
        }

        public ClaveLocal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<ClaveLocal> GetAll()
        {
            List<ClaveLocal> claves = new List<ClaveLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM clave order by id desc", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var claveLocal = new ClaveLocal();
                        claveLocal.Id = int.Parse(rd["id"].ToString());
                        claveLocal.Clave = rd["clave"] as string;
                        claveLocal.Cuota = rd["cuota"] as string;
                        claveLocal.Precio = decimal.Parse(rd["precio"].ToString());
                        claves.Add(claveLocal);
                    }
                }
            }
            return claves;
        }

        public ClaveLocal GetByClave(string clave)
        {
            ClaveLocal claveLocal = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM clave WHERE clave=@clave", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@clave", clave);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        claveLocal = new ClaveLocal();
                        claveLocal.Id = int.Parse(rd["id"].ToString());
                        claveLocal.Clave = rd["clave"] as string;
                        claveLocal.Cuota = rd["cuota"] as string;
                        claveLocal.Precio = decimal.Parse(rd["precio"].ToString()); 
                    }
                } 
            } 
            return claveLocal;
        }

        public bool Update(ClaveLocal claveLocal)
        {
            bool inserted = false; 
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update clave set clave=@clave,cuota=@cuota,precio=@precio WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", claveLocal.Id);
                    cmd.Parameters.AddWithValue("@clave", claveLocal.Clave);
                    cmd.Parameters.AddWithValue("@cuota", claveLocal.Cuota);
                    cmd.Parameters.AddWithValue("@precio", claveLocal.Precio);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;

                }
            }
            return inserted;
        }
    }
}
