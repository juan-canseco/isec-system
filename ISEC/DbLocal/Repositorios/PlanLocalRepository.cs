using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEC.DbLocal.Interfaces;
using System.Data.SQLite;
using System.Data;
using DataAccess.Local;

namespace ISEC.DbLocal.Repositorios
{
    public class PlanLocalRepository : IPlanLocalRepository
    {
        private string connection = ConnectionLocal.ConecctionString;
        public bool Add(PlanLocal planLocal)
        { 
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into plan (descripcion,fecha) values(@desc,@fecha)", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    cmd.Parameters.AddWithValue("@desc", planLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@fecha", planLocal.Fecha);
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
        public PlanLocal Get(int id)
        {
            throw new NotImplementedException();
        } 
        public List<PlanLocal> GetAll()
        {
            List<PlanLocal> planes = new List<PlanLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from plan order by id desc", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var plan = new PlanLocal();
                        plan.Id = int.Parse(rd["id"].ToString());
                        plan.Descripcion = rd["descripcion"] as string;
                        plan.Fecha = DateTime.Parse(rd["fecha"].ToString()).ToString();
                        plan.EsActivo = int.Parse(rd["activo"].ToString());
                        planes.Add(plan);
                    }
                } 
            }
            return planes;
        } 
        public bool Update(PlanLocal planLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update plan set descripcion=@desc,fecha=@fecha,activo=@activo where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", planLocal.Id);
                    cmd.Parameters.AddWithValue("@desc", planLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@fecha", planLocal.Fecha);
                    cmd.Parameters.AddWithValue("@activo", planLocal.EsActivo);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }

            }
            return inserted;
        }
    }
}
