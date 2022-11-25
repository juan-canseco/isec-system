using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ISEC.DbLocal.Interfaces;
using DataAccess.Local;
using System.Data.SQLite;
using System.Data;
using System.Globalization;
using ISEC.Utilidades;

namespace ISEC.DbLocal.Repositorios
{
    public class CobranzaLocalRepository : ICobranzaLocalRepository
    {
        string connection = ConnectionLocal.ConecctionString;
        public bool Add(CobranzaLocal cobranzaLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into cobranza (folio,username,fondoinicial,fechainicial,fechafinal,sobrante,faltante,ingreso,egreso,estado,saldocaja) " +
               "VALUES (@folio,@username,@fondo,@fi,@ff,@s,@f,@in,@eg,@estado,@saldocaja) ", conn))
                {
                    cmd.Parameters.AddWithValue("@folio", cobranzaLocal.Folio);
                    cmd.Parameters.AddWithValue("@username", cobranzaLocal.Username);
                    cmd.Parameters.AddWithValue("@fondo", cobranzaLocal.FondoInicial);
                    cmd.Parameters.AddWithValue("@fi", cobranzaLocal.FechaInicial);
                    cmd.Parameters.AddWithValue("@ff", cobranzaLocal.FechaFinal);
                    cmd.Parameters.AddWithValue("@s", cobranzaLocal.Sobrante);
                    cmd.Parameters.AddWithValue("@f", cobranzaLocal.Faltante);
                    cmd.Parameters.AddWithValue("@in", cobranzaLocal.Ingreso);
                    cmd.Parameters.AddWithValue("@eg", cobranzaLocal.Egreso);
                    cmd.Parameters.AddWithValue("@estado", cobranzaLocal.Estado);
                    cmd.Parameters.AddWithValue("@saldocaja", cobranzaLocal.SaldoCaja);
                    cmd.CommandType = CommandType.Text;
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
                conn.Close();
            }
            return inserted;
        }

        public List<CobranzaLocal> All()
        {
            throw new NotImplementedException();
        }

        public bool Delete(int id)
        {
            throw new NotImplementedException();
        }


        public int Exists()
        {
            int id = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select fechainicial  from cobranza ORDER BY id desc limit 1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        if (!string.IsNullOrEmpty(rd[0].ToString()))
                        {
                            string val = rd[0].ToString();
                            DateTime dt = DateTime.Parse(val);
                            if (dt.Date == DateTime.Now.Date)
                            {
                                id = 1;
                            }
                            else
                            {
                                id = 0;
                            }
                        }
                        else
                        {
                            id = 0;
                        }

                    }
                }
            }
            return id;
        }

        public CobranzaLocal Get(int id)
        {
            CobranzaLocal cobranza = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM cobranza where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        cobranza = new CobranzaLocal();
                        cobranza.Id = int.Parse(rd["id"].ToString());
                        cobranza.Folio = rd["folio"] as string;
                        cobranza.Username = rd["username"] as string;
                        cobranza.FondoInicial = decimal.Parse(rd["fondoinicial"].ToString());
                        cobranza.FechaInicial = rd["fechainicial"].ToString() as string;
                        cobranza.FechaFinal = rd["fechafinal"].ToString() as string;
                        cobranza.Sobrante = decimal.Parse(rd["sobrante"].ToString());
                        cobranza.Faltante = decimal.Parse(rd["faltante"].ToString());
                        cobranza.Ingreso = decimal.Parse(rd["ingreso"].ToString());
                        cobranza.Egreso = decimal.Parse(rd["egreso"].ToString());
                        cobranza.Estado = int.Parse(rd["estado"].ToString());
                        cobranza.SaldoCaja = decimal.Parse(rd["saldocaja"].ToString());
                    }
                }
                return cobranza;
            }
        }

        public int GetLast()
        {
            int last = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT id FROM cobranza ORDER BY id desc limit 1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = int.Parse(rd[0].ToString());
                    }
                }
                return last;
            }
        }

        public CobranzaLocal GetLastCobranza()
        {
            CobranzaLocal cobranza = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand($"select * from cobranza where id={GetLast()}",conn))
                {
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        cobranza = new CobranzaLocal();
                        cobranza.Id = int.Parse(rd["id"].ToString());
                        cobranza.Folio = rd["folio"] as string;
                        cobranza.Username = rd["username"] as string;
                        cobranza.FondoInicial = decimal.Parse(rd["fondoinicial"].ToString());
                        cobranza.FechaInicial =rd["fechainicial"].ToString() as string;
                        cobranza.FechaFinal =rd["fechafinal"].ToString() as string;
                        cobranza.Sobrante = decimal.Parse(rd["sobrante"].ToString());
                        cobranza.Faltante = decimal.Parse(rd["faltante"].ToString());
                        cobranza.Ingreso = decimal.Parse(rd["ingreso"].ToString());
                        cobranza.Egreso = decimal.Parse(rd["egreso"].ToString());
                        cobranza.Estado = int.Parse(rd["estado"].ToString());
                        cobranza.SaldoCaja = decimal.Parse(rd["saldocaja"].ToString());

                    }
                }
                return cobranza;
            }
        }

        public int GetLastFolio()
        {
            int last = 0;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT folio FROM cobranza ORDER BY folio desc limit 1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        last = int.Parse(rd[0].ToString());
                    }
                }
                return last;
            }
        }

        public bool RestaQuantity(decimal quantity, int id)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update cobranza SET saldocaja=@saldo WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@saldo", quantity);
                    cmd.Parameters.AddWithValue("@id", id);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
            }
            return updated;
        }

        public bool SumQuantity(decimal quantity)
        {
            bool sum = false;
            if (Exists() > 0)
            {
                int lastid = GetLast();
                if (lastid > 0)
                {
                    using (var conn =new SQLiteConnection(connection))
                    {
                        conn.Open();
                        decimal fi = 0;
                        decimal saldo = 0;
                        using (var cmd = new SQLiteCommand("select fondoinicial,saldocaja from cobranza where id=@id", conn))
                        {
                            cmd.CommandType = CommandType.Text; 
                            cmd.Parameters.AddWithValue("@id", lastid);
                            var result = cmd.ExecuteReader();
                            if (result.Read())
                            {
                                fi = decimal.Parse(result[0].ToString());
                                saldo = decimal.Parse(result[1].ToString()); 
                            }
                        }
                        conn.Close();
                        conn.Open();
                        using (var cmd = new SQLiteCommand("update cobranza SET fondoinicial=@fi,saldocaja=@saldo WHERE id=@id", conn))
                        {
                            cmd.CommandType = CommandType.Text;
                            cmd.Parameters.AddWithValue("@fi", quantity+fi);
                            cmd.Parameters.AddWithValue("@saldo", quantity+saldo);
                            cmd.Parameters.AddWithValue("@id", lastid);
                            var result = cmd.ExecuteNonQuery();
                            sum = result > 0 ? true : false;
                        }
                    }
                }
            }
            return sum;

        }

        public bool Update(CobranzaLocal cobranzaLocal)
        {
            throw new NotImplementedException();
        }
    }
}
