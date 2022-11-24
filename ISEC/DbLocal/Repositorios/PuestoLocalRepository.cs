using ISEC.DbLocal.Interfaces;
using DataAccess.Local; 
using System.Collections.Generic; 
using System.Data.SQLite;

namespace ISEC.DbLocal.Repositorios
{
    public class PuestoLocalRepository : IPuestoLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;
        public bool Add(PuestoLocal puestoLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into puesto (descripcion) values (@desc)", conn))
                {
                    cmd.Parameters.AddWithValue("@desc", puestoLocal.Descripcion);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }

        public PuestoLocal Get(int id)
        {
            PuestoLocal puesto = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from puesto where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        puesto = new PuestoLocal();
                        puesto.Id = int.Parse(rd["id"].ToString());
                        puesto.Descripcion = rd["descripcion"] as string;
                    }
                }
                return puesto;
            }
        }

        public List<PuestoLocal> GetAll()
        {
            List<PuestoLocal> puestos = new List<PuestoLocal>();

            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from puesto order by id desc", conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var puesto = new PuestoLocal();
                        puesto.Id = int.Parse(rd["id"].ToString());
                        puesto.Descripcion = rd["descripcion"] as string;
                        puestos.Add(puesto);
                    }
                }
                return puestos;
            }
        }

        public bool Update(PuestoLocal puestoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update puesto set descripcion=@desc where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", puestoLocal.Id);
                    cmd.Parameters.AddWithValue("@desc", puestoLocal.Descripcion);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }
    }
}
