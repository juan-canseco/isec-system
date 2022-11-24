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
    public class AlumnoLocalRepository : IAlumnoLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString; 
        public bool Add(AlumnoLocal alumnoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("INSERT INTO alumno (credencial,nombre,direccion,telefono,carreraid,cuotaid,fechainicio,consejero,semana,activo,usuario,baja,fechabaja,reingreso,fechareingreso,fkgrupo,fkplan) " +
                    "VALUES(@cre,@nom,@dir,@tel,@carreraid,@cuotaid,@fi,@consejero,@semana,@activo,@usuario,@baja,@fb,@reingreso,@fr,@fkgrupo,@fkplan)", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cre", alumnoLocal.Credencial);
                    cmd.Parameters.AddWithValue("@nom", alumnoLocal.Nombre);
                    cmd.Parameters.AddWithValue("@dir", alumnoLocal.Direccion);
                    cmd.Parameters.AddWithValue("@tel", alumnoLocal.Telefono);
                    cmd.Parameters.AddWithValue("@carreraid", alumnoLocal.CarreraId);
                    cmd.Parameters.AddWithValue("@cuotaid", alumnoLocal.CuotaId);
                    cmd.Parameters.AddWithValue("@fi", alumnoLocal.FechaIngreso);
                    cmd.Parameters.AddWithValue("@consejero", alumnoLocal.Consejero);
                    cmd.Parameters.AddWithValue("@semana", alumnoLocal.Semana);
                    cmd.Parameters.AddWithValue("@activo", alumnoLocal.EsActivo);
                    cmd.Parameters.AddWithValue("@usuario", alumnoLocal.Usuario);
                    cmd.Parameters.AddWithValue("@baja", alumnoLocal.Baja);
                    cmd.Parameters.AddWithValue("@fb", alumnoLocal.FechaBaja);
                    cmd.Parameters.AddWithValue("@reingreso", alumnoLocal.Reingreso);
                    cmd.Parameters.AddWithValue("@fr", alumnoLocal.FechaReingreso);
                    cmd.Parameters.AddWithValue("@fkgrupo", alumnoLocal.FkGrupo);
                    cmd.Parameters.AddWithValue("@fkplan", alumnoLocal.FkPlan);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;

                }
            }
            return updated;
        }

        public bool Desactivate(int id)
        {
            throw new NotImplementedException();
        }

        public AlumnoLocal Get(int id)
        {
            AlumnoLocal alumno = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM alumno WHERE id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        alumno = new AlumnoLocal();
                        alumno.Id = int.Parse(rd["id"].ToString());
                        alumno.Credencial = rd["credencial"].ToString();
                        alumno.Nombre = rd["nombre"] as string;
                        alumno.Direccion = rd["direccion"] as string;
                        alumno.Telefono = rd["telefono"] as string;
                        alumno.CarreraId = int.Parse(rd["carreraid"].ToString());
                        alumno.CuotaId = int.Parse(rd["cuotaid"].ToString());
                        alumno.FechaIngreso = rd["fechainicio"] as string;
                        alumno.Consejero = rd["consejero"] as string;
                        alumno.Semana = int.Parse(rd["semana"].ToString());
                        alumno.EsActivo = int.Parse(rd["activo"].ToString());
                        alumno.Usuario = rd["usuario"] as string;
                        alumno.Baja = int.Parse(rd["baja"].ToString());
                        alumno.FechaBaja = rd["fechabaja"] as string;
                        alumno.Reingreso = int.Parse(rd["reingreso"].ToString());
                        alumno.FechaReingreso = rd["fechareingreso"] as string; 
                        alumno.FkGrupo = int.Parse(rd["fkgrupo"].ToString()); 
                        alumno.FkPlan =int.Parse( rd["fkplan"].ToString()); 
                    }

                }
            }
            return alumno;
        }

        public List<AlumnoLocal> GetAll()
        {
            List<AlumnoLocal> alumnos = new List<AlumnoLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM alumno order by id desc", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        AlumnoLocal alumno = new AlumnoLocal();
                        alumno.Id = int.Parse(rd["id"].ToString());
                        alumno.Credencial = rd["credencial"].ToString();
                        alumno.Nombre = rd["nombre"] as string;
                        alumno.Direccion = rd["direccion"] as string;
                        alumno.Telefono = rd["telefono"] as string;
                        alumno.CarreraId = int.Parse(rd["carreraid"].ToString());
                        alumno.CuotaId = int.Parse(rd["cuotaid"].ToString());
                        alumno.FechaIngreso =!string.IsNullOrEmpty(rd["fechainicio"].ToString())?  rd["fechainicio"] as string : null;
                        alumno.Consejero = rd["consejero"] as string;
                        alumno.Semana = int.Parse(rd["semana"].ToString());
                        alumno.EsActivo = int.Parse(rd["activo"].ToString());
                        alumno.Usuario = rd["usuario"] as string;
                        alumno.Baja = int.Parse(rd["baja"].ToString());
                        alumno.FechaBaja = !string.IsNullOrEmpty(rd["fechabaja"].ToString()) ? rd["fechabaja"] as string : null;
                        alumno.Reingreso = int.Parse(rd["reingreso"].ToString());
                        alumno.FechaReingreso = !string.IsNullOrEmpty(rd["fechareingreso"].ToString()) ? rd["fechareingreso"] as string : null;
                        alumno.FkGrupo = !string.IsNullOrEmpty(rd["fkgrupo"].ToString()) ? int.Parse(rd["fkgrupo"].ToString()) :0;
                        alumno.FkPlan = !string.IsNullOrEmpty(rd["fkplan"].ToString()) ? int.Parse(rd["fkplan"].ToString()) :0;
                        alumnos.Add(alumno);                        
                    }

                }
            }
            return alumnos;
        }

        public AlumnoLocal GetByCredencial(string credencial)
        {
            AlumnoLocal alumno = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM alumno WHERE credencial=@cr", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@cr", credencial);
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        alumno = new AlumnoLocal();
                        alumno.Id = int.Parse(rd["id"].ToString());
                        alumno.Credencial = rd["credencial"].ToString();
                        alumno.Nombre = rd["nombre"] as string;
                        alumno.Direccion = rd["direccion"] as string;
                        alumno.Telefono = rd["telefono"] as string;
                        alumno.CarreraId = int.Parse(rd["carreraid"].ToString());
                        alumno.CuotaId = int.Parse(rd["cuotaid"].ToString());
                        alumno.FechaIngreso = rd["fechainicio"] as string;
                        alumno.Consejero = rd["consejero"] as string;
                        alumno.Semana = int.Parse(rd["semana"].ToString());
                        alumno.EsActivo = int.Parse(rd["activo"].ToString());
                        alumno.Usuario = rd["usuario"] as string;
                        alumno.Baja = int.Parse(rd["baja"].ToString());
                        alumno.FechaBaja = rd["fechabaja"] as string;
                        alumno.Reingreso = int.Parse(rd["reingreso"].ToString());
                        alumno.FechaReingreso = rd["fechareingreso"] as string;
                        alumno.FkGrupo = int.Parse(rd["fkgrupo"].ToString());
                        alumno.FkPlan = int.Parse(rd["fkplan"].ToString());
                       
                    }

                }
            }
            return alumno;
        }

        public AlumnoLocal GetLast()
        {
            AlumnoLocal alumno = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM alumno ORDER BY id DESC", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        alumno = new AlumnoLocal();
                        alumno.Id = int.Parse(rd["id"].ToString());
                        alumno.Credencial = rd["credencial"].ToString();
                        alumno.Nombre = rd["nombre"] as string;
                        alumno.Direccion = rd["direccion"] as string;
                        alumno.Telefono = rd["telefono"] as string;
                        alumno.CarreraId = int.Parse(rd["carreraid"].ToString());
                        alumno.CuotaId = int.Parse(rd["cuotaid"].ToString());
                        alumno.FechaIngreso = rd["fechainicio"] as string;
                        alumno.Consejero = rd["consejero"] as string;
                        alumno.Semana = int.Parse(rd["semana"].ToString());
                        alumno.EsActivo = int.Parse(rd["activo"].ToString());
                        alumno.Usuario = rd["usuario"] as string;
                        alumno.Baja = int.Parse(rd["baja"].ToString());
                        alumno.FechaBaja = rd["fechabaja"] as string;
                        alumno.Reingreso = int.Parse(rd["reingreso"].ToString());
                        alumno.FechaReingreso = rd["fechareingreso"] as string;
                        alumno.FkGrupo = int.Parse(rd["fkgrupo"].ToString());
                        alumno.FkPlan = int.Parse(rd["fkplan"].ToString());
                    }

                }
            }
            return alumno;
        }

        public bool Update(AlumnoLocal alumnoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("UPDATE alumno SET credencial=@cre,nombre=@nom,direccion=@dir,telefono=@tel,carreraid=@carreraid,cuotaid=@cuotaid,fechainicio=@fi," +
                    "consejero=@consejero,semana=@semana,activo=@activo,usuario=@usuario,baja=@baja,fechabaja=@fb,reingreso=@reingreso,fechareingreso=@fr,fkgrupo=@fkg,fkplan=@fkp WHERE id=@idd", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@idd", alumnoLocal.Id);
                    cmd.Parameters.AddWithValue("@cre", alumnoLocal.Credencial);
                    cmd.Parameters.AddWithValue("@nom", alumnoLocal.Nombre);
                    cmd.Parameters.AddWithValue("@dir", alumnoLocal.Direccion);
                    cmd.Parameters.AddWithValue("@tel", alumnoLocal.Telefono);
                    cmd.Parameters.AddWithValue("@carreraid", alumnoLocal.CarreraId);
                    cmd.Parameters.AddWithValue("@cuotaid", alumnoLocal.CuotaId);
                    cmd.Parameters.AddWithValue("@fi", alumnoLocal.FechaIngreso);
                    cmd.Parameters.AddWithValue("@consejero", alumnoLocal.Consejero);
                    cmd.Parameters.AddWithValue("@semana", alumnoLocal.Semana);
                    cmd.Parameters.AddWithValue("@activo", alumnoLocal.EsActivo);
                    cmd.Parameters.AddWithValue("@usuario", alumnoLocal.Usuario);
                    cmd.Parameters.AddWithValue("@baja", alumnoLocal.Baja);
                    cmd.Parameters.AddWithValue("@fb", alumnoLocal.FechaBaja);
                    cmd.Parameters.AddWithValue("@reingreso", alumnoLocal.Reingreso);
                    cmd.Parameters.AddWithValue("@fr", alumnoLocal.FechaReingreso);
                    cmd.Parameters.AddWithValue("@fkg", alumnoLocal.FkGrupo);
                    cmd.Parameters.AddWithValue("@fkp", alumnoLocal.FkPlan);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;

                } 
            } 
            return updated;
        }
    }
}
