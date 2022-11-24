using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;

namespace ISEC.DbLocal.Repositorios
{
    public class CarreraLocalRepository : ICarreraLocalRepository
    {
        private string connection = ConnectionLocal.ConecctionString;
        public bool Add(CarreraLocal carreraLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into carrera (nombre) values (@nombre)",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@nombre", carreraLocal.Nombre);
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

        public CarreraLocal Get(int id)
        {
            throw new NotImplementedException();
        }

        public List<CarreraLocal> GetAll()
        {
            List<CarreraLocal> carreras = new List<CarreraLocal>();
            using (var conn =new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from carrera order by id desc",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        CarreraLocal carrera = new CarreraLocal();
                        carrera.Id = int.Parse(rd["id"].ToString());
                        carrera.Nombre = rd["nombre"] as string;
                        carrera.Activa = int.Parse(rd["activa"].ToString()) > 0 ? true : false;
                        carreras.Add(carrera);
                    }
                }
            }
            return carreras;
        }

        public bool Update(CarreraLocal carreraLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update carrera SET nombre=@nombre,activa=@activa where id=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", carreraLocal.Id);
                    cmd.Parameters.AddWithValue("@nombre", carreraLocal.Nombre);
                    cmd.Parameters.AddWithValue("@activa", carreraLocal.Activa ? 1 :0);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }

            }
            return inserted;
        }
    }
}
