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
    public class HorarioLocalRepository : IHorarioLocalRepository
    {
        private string connection = ConnectionLocal.ConecctionString;
        public bool Add(HorarioLocal horarioLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into horario (descripcion,lunes,martes,miercoles,jueves,viernes,sabado,domingo,horainicial,horafinal) " +
                    "values (@desc,@l,@ma,@mi,@j,@v,@s,@d,@hi,@hf)",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@desc", horarioLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@l", horarioLocal.Lunes);
                    cmd.Parameters.AddWithValue("@ma", horarioLocal.Martes);
                    cmd.Parameters.AddWithValue("@mi", horarioLocal.Miercoles);
                    cmd.Parameters.AddWithValue("@j", horarioLocal.Jueves);
                    cmd.Parameters.AddWithValue("@v", horarioLocal.Viernes);
                    cmd.Parameters.AddWithValue("@s", horarioLocal.Sabado);
                    cmd.Parameters.AddWithValue("@d", horarioLocal.Domingo);
                    cmd.Parameters.AddWithValue("@hi", horarioLocal.HoraInicial);
                    cmd.Parameters.AddWithValue("@hf", horarioLocal.HoraFinal);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }

            }
            return inserted;
        }

        public bool Desactivate(int id)
        {
            return false;
        }

        public HorarioLocal Get(int id)
        {
            HorarioLocal horario = new HorarioLocal();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from horario where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        horario = new HorarioLocal();
                        horario.Id = int.Parse(rd["id"].ToString());
                        horario.Descripcion = rd["descripcion"] as string;
                        horario.Lunes = int.Parse(rd["lunes"].ToString());
                        horario.Martes = int.Parse(rd["martes"].ToString());
                        horario.Miercoles = int.Parse(rd["miercoles"].ToString());
                        horario.Jueves = int.Parse(rd["jueves"].ToString());
                        horario.Viernes = int.Parse(rd["viernes"].ToString());
                        horario.Sabado = int.Parse(rd["sabado"].ToString());
                        horario.Domingo = int.Parse(rd["domingo"].ToString());
                        horario.HoraInicial = rd["horainicial"] as string;
                        horario.HoraFinal = rd["horafinal"] as string;
                        horario.Activo = int.Parse(rd["activo"].ToString()); 
                    }
                }

            }

            return horario;
        }

        public List<HorarioLocal> GetAll()
        {
            List<HorarioLocal> horarios = new List<HorarioLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from horario", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        HorarioLocal horario = new HorarioLocal();
                        horario.Id = int.Parse(rd["id"].ToString());
                        horario.Descripcion = rd["descripcion"] as string;
                        horario.Lunes = int.Parse(rd["lunes"].ToString());
                        horario.Martes = int.Parse(rd["martes"].ToString());
                        horario.Miercoles = int.Parse(rd["miercoles"].ToString());
                        horario.Jueves = int.Parse(rd["jueves"].ToString());
                        horario.Viernes = int.Parse(rd["viernes"].ToString());
                        horario.Sabado = int.Parse(rd["sabado"].ToString());
                        horario.Domingo = int.Parse(rd["domingo"].ToString());
                        horario.HoraInicial = rd["horainicial"] as string;
                        horario.HoraFinal = rd["horafinal"] as string;
                        horario.Activo = int.Parse(rd["activo"].ToString());
                        horarios.Add(horario);
                    }
                }

            }

            return horarios;
        }

        public bool Update(HorarioLocal horarioLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update horario  set descripcion=@desc,lunes=@l,martes=@ma,miercoles=@mi,jueves=@j,viernes=@v,sabado=@s,domingo=@d,horainicial=@hi,horafinal=@hf " +
                    "where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", horarioLocal.Id);
                    cmd.Parameters.AddWithValue("@desc", horarioLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@l", horarioLocal.Lunes);
                    cmd.Parameters.AddWithValue("@ma", horarioLocal.Martes);
                    cmd.Parameters.AddWithValue("@mi", horarioLocal.Miercoles);
                    cmd.Parameters.AddWithValue("@j", horarioLocal.Jueves);
                    cmd.Parameters.AddWithValue("@v", horarioLocal.Viernes);
                    cmd.Parameters.AddWithValue("@s", horarioLocal.Sabado);
                    cmd.Parameters.AddWithValue("@d", horarioLocal.Domingo);
                    cmd.Parameters.AddWithValue("@hi", horarioLocal.HoraInicial);
                    cmd.Parameters.AddWithValue("@hf", horarioLocal.HoraFinal);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }

            }
            return updated;
        }
    }
}
