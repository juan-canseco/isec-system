using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System.Data;
namespace ISEC.DbLocal.Repositorios
{
    public class CuotaLocalRepository : ICuotaRepository
    { 
        string connection = ConnectionLocal.ConecctionString;
        public bool Add(CuotaLocal cuotaLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO cuota (tipo,descripcion,colegiatura) VALUES(@tipo,@descripcion,@colegiatura)",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@tipo",cuotaLocal.Tipo);
                    cmd.Parameters.AddWithValue("@descripcion",cuotaLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@colegiatura",cuotaLocal.Colegiatura); 
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                } 
            } 
            return inserted;
        }
        
        public bool Desactivate(int id)
        {
            bool desactivated = false;
            using (var conn = new SQLiteConnection(connection))
            {

            }
            return desactivated;
        }

        public CuotaLocal Get(int id)
        {
            CuotaLocal cuota = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM cuota WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        cuota = new CuotaLocal();
                        cuota.Id = int.Parse(rd["id"].ToString());
                        cuota.Tipo = rd["tipo"] as string;
                        cuota.Descripcion = rd["descripcion"] as string;
                        cuota.Colegiatura = decimal.Parse(rd["colegiatura"].ToString());
                        cuota.Activa = int.Parse(rd["activa"].ToString())>0 ?true :false;
                    }
                }
            }
            return cuota;
        }

        public List<CuotaLocal> GetAll()
        {
            List<CuotaLocal> cuotas = new List<CuotaLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM cuota order by id desc", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        CuotaLocal cuota = new CuotaLocal();
                        cuota.Id = int.Parse(rd["id"].ToString());
                        cuota.Tipo = rd["tipo"] as string;
                        cuota.Descripcion = rd["descripcion"] as string;
                        cuota.Colegiatura = decimal.Parse(rd["colegiatura"].ToString());
                        cuota.Activa = int.Parse(rd["activa"].ToString()) > 0 ? true : false ;
                        cuotas.Add(cuota);
                    }
                 }
            } 
            return cuotas;
        }

        public bool Update(CuotaLocal cuotaLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("UPDATE cuota SET tipo=@tipo,descripcion=@descripcion,colegiatura=@colegiatura,activa=@activa WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", cuotaLocal.Id);
                    cmd.Parameters.AddWithValue("@tipo", cuotaLocal.Tipo);
                    cmd.Parameters.AddWithValue("@descripcion", cuotaLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@colegiatura", cuotaLocal.Colegiatura);
                    cmd.Parameters.AddWithValue("@activa", cuotaLocal.Activa);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }
    }
}
