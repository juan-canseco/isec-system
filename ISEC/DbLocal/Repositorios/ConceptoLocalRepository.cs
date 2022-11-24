using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
namespace ISEC.DbLocal.Repositorios
{
    public class ConceptoLocalRepository : IConceptoLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;
        public bool Add(ConceptoLocal conceptoLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into concepto (descripcion) values(@con)",conn))
                {
                    cmd.Parameters.AddWithValue("@con", conceptoLocal.Descripcion);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }

        public ConceptoLocal Get(int id)
        {
            ConceptoLocal concepto = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from concepto where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        concepto = new ConceptoLocal();
                        concepto.Id = int.Parse(rd["id"].ToString());
                        concepto.Descripcion = rd["descripcion"] as string; 
                    }
                }
                return concepto;
            }
        }

        public List<ConceptoLocal> GetAll()
        {
            List<ConceptoLocal> conceptos = new List<ConceptoLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from concepto order by id desc",conn))
                {
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        ConceptoLocal concepto = new ConceptoLocal();
                        concepto.Id = int.Parse(rd["id"].ToString());
                        concepto.Descripcion = rd["descripcion"] as string;
                        conceptos.Add(concepto);
                    }
                }
                return conceptos;
            }
        }

        public bool Update(ConceptoLocal conceptoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update concepto set descripcion=@con where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", conceptoLocal.Id);
                    cmd.Parameters.AddWithValue("@con", conceptoLocal.Descripcion);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }
    }
}
