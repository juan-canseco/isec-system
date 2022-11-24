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
    public class CobranzaDetalleLocalRepository : ICobranzaDetalleLocalRepository
    {
        private string connection = ConnectionLocal.ConecctionString;
        public bool Add(CobranzaDetalleLocal pago)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into cobranza_detalle (folio,fkcobranza,fkalumno,fecha,esingreso,esegreso,cantidad,semanas,clave,username,resto,week1,week2,dtweek1,dtweek2) " +
                    "values (@folio,@fkc,@fka,@fecha,@esi,@ese,@cantidad,@semanas,@clave,@username,@resto,@week1,@week2,@dtw1,@dtw2)",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@folio", pago.Folio);
                    cmd.Parameters.AddWithValue("@fkc", pago.FkCobranza);
                    cmd.Parameters.AddWithValue("@fka", pago.FkAlumno);
                    cmd.Parameters.AddWithValue("@fecha", pago.Fecha);
                    cmd.Parameters.AddWithValue("@esi", pago.EsIngreso);
                    cmd.Parameters.AddWithValue("@ese", pago.EsEgreso);
                    cmd.Parameters.AddWithValue("@cantidad", pago.Cantidad);
                    cmd.Parameters.AddWithValue("@semanas", pago.Semanas);
                    cmd.Parameters.AddWithValue("@clave", pago.Clave);
                    cmd.Parameters.AddWithValue("@username", pago.Username);
                    cmd.Parameters.AddWithValue("@resto", pago.Resto); 
                    cmd.Parameters.AddWithValue("@week1", pago.Week1);
                    cmd.Parameters.AddWithValue("@week2", pago.Week2);
                    cmd.Parameters.AddWithValue("@dtw1", pago.DtWeek1);
                    cmd.Parameters.AddWithValue("@dtw2", pago.DtWeek2);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
            }
            return inserted;
        }

        public CobranzaDetalleLocal Get(int id)
        {
            throw new NotImplementedException();
        }

        public PagoLocalViewModel GetLastPago()
        {
            PagoLocalViewModel pago = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from v_UltimoPago",conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        pago = new PagoLocalViewModel();
                        pago.Folio = rd["folio"] as string;
                        pago.Credencial = rd[1].ToString();
                        pago.Nombre = rd["nombre"] as string;
                        pago.Direccion = rd["direccion"] as string;
                        pago.Cantidad = decimal.Parse(rd["cantidad"].ToString());
                        pago.Resto = decimal.Parse(rd["resto"].ToString());
                        pago.Fecha = rd["fecha"] as string;
                        pago.Clave = rd["clave"] as string;
                        pago.Cuota = rd["cuota"] as string;
                        pago.Username = rd["username"] as string;
                        pago.FkAlumno = int.Parse(rd["fkalumno"].ToString());
                        pago.Grupo = rd["grupo"] as string;
                        pago.Horario = rd["horario"] as string;
                        pago.Carrera = rd["carrera"] as string;
                        pago.Cuotaa = rd["cuotaa"] as string;
                        pago.Plan = rd["plan"]as string;
                        pago.Week1 = int.Parse(rd["week1"].ToString());
                        pago.Week2 = int.Parse(rd["week2"].ToString());
                        pago.DtWeek1 = rd["dtweek1"] as string;
                        pago.DtWeek2 = rd["dtweek2"] as string; 
                    }
                }
            }

            return pago;
        }
        
        public List<PagoLocalViewModel> HistorialPagosPorAlumno(int fkalumno)
        {
            List<PagoLocalViewModel> pagos = new List<PagoLocalViewModel>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from v_PagosPorAlumno where fkalumno={fkalumno}", conn))
                {
                    cmd.CommandType = CommandType.Text; 
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var pago = new PagoLocalViewModel();
                        pago.Folio = rd["folio"] as string;
                        pago.Credencial = rd[1].ToString();
                        pago.Nombre = rd["nombre"] as string;
                        pago.Direccion = rd["direccion"] as string;
                        pago.Cantidad = decimal.Parse(rd["cantidad"].ToString());
                        pago.Resto = decimal.Parse(rd["resto"].ToString());
                        pago.Fecha = rd["fecha"] as string;
                        pago.Clave = rd["clave"] as string;
                        pago.Cuota = rd["cuota"] as string;
                        pago.Username = rd["username"] as string;
                        pago.FkAlumno = int.Parse(rd["fkalumno"].ToString());
                        pago.Grupo = rd["grupo"] as string;
                        pago.Horario = rd["horario"] as string;
                        pago.Carrera = rd["carrera"] as string;
                        pago.Cuotaa = rd["cuotaa"] as string;
                        pago.Plan = rd["plan"] as string;
                        pago.Week1 = int.Parse(rd["week1"].ToString());
                        pago.Week2 = int.Parse(rd["week2"].ToString());
                        pago.DtWeek1 = rd["dtweek1"] as string;
                        pago.DtWeek2 = rd["dtweek2"] as string;
                        pagos.Add(pago);

                    }
                }
            }

            return pagos;
        }

        public List<CobranzaDetalleLocal> GetAll()
        {
            throw new NotImplementedException();
        }

        public string Last()
        {
            string last = String.Empty;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select folio from cobranza_detalle order by id desc limit 1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = rd[0].ToString();
                    }
                }
            }
            return last;
        }

        public bool Update(CobranzaDetalleLocal pago)
        {
            throw new NotImplementedException();
        }

        public int GetWeekForPay(int alumnoId)
        {
            int weeks = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select sum(semanas) from cobranza_detalle where fkalumno=@id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@id", alumnoId);
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        weeks = !string.IsNullOrEmpty(rd[0].ToString())?  int.Parse(rd[0].ToString()) :0;
                    }
                }
            }

            return weeks;
        }
    }
}
