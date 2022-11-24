using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System.Data.SQLite;
using System.Data;
namespace ISEC.DbLocal.Repositorios
{
    public class GrupoLocalRepository : IGrupoLocalRepository
    { 
        private string connection =  ConnectionLocal.ConecctionString;
        private HorarioLocalRepository horarioLocalRepository = new HorarioLocalRepository();
        public bool Add(GrupoLocal grupoLocal)
        { 
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into grupo (numero,letra,fkhorario) values(@n,@letra,@fk)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@n", grupoLocal.Numero);
                    cmd.Parameters.AddWithValue("@letra", grupoLocal.Letra);
                    cmd.Parameters.AddWithValue("@fk", grupoLocal.FkHorario);
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

        public GrupoLocal Get(int id)
        {
            GrupoLocal grupo = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from grupo where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        grupo = new GrupoLocal();
                        grupo.Id = int.Parse(rd["id"].ToString());
                        grupo.Numero = int.Parse(rd["numero"].ToString());
                        grupo.Letra = rd["letra"] as string;
                        grupo.FkHorario = int.Parse(rd["fkhorario"].ToString());
                        grupo.EsActivo = int.Parse(rd["esactivo"].ToString());
                        grupo.HorarioObj = horarioLocalRepository.Get(grupo.FkHorario);
                       
                    }
                }

            }
            return grupo;
        }

        public List<GrupoLocal> GetAll()
        {
            List<GrupoLocal> grupos = new List<GrupoLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from grupo order by id desc", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        GrupoLocal grupo = new GrupoLocal();
                        grupo.Id = int.Parse(rd["id"].ToString());
                        grupo.Numero = int.Parse(rd["numero"].ToString());
                        grupo.Letra = rd["letra"] as string;
                        grupo.FkHorario = int.Parse(rd["fkhorario"].ToString());
                        grupo.EsActivo = int.Parse(rd["esactivo"].ToString());
                        grupo.HorarioObj = horarioLocalRepository.Get(grupo.FkHorario); 
                        grupos.Add(grupo);
                    }
                }

            }
            return grupos;
        }

        public bool Update(GrupoLocal grupoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update grupo set numero=@n,letra=@letra,fkhorario=@fk where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", grupoLocal.Id);
                    cmd.Parameters.AddWithValue("@n", grupoLocal.Numero);
                    cmd.Parameters.AddWithValue("@letra", grupoLocal.Letra);
                    cmd.Parameters.AddWithValue("@fk", grupoLocal.FkHorario);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }

            }
            return updated;
        }
    }
}
