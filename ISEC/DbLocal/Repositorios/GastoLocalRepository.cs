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
    public class GastoLocalRepository : IGastoLocalRepository
    {
        CobranzaLocalRepository cobranzaRepo = new CobranzaLocalRepository();
        ConceptoLocalRepository conceptoRepo = new ConceptoLocalRepository();
        string connection = ConnectionLocal.ConecctionString;
        decimal suma = 0;

        public bool Add(GastoLocal gastoLocal)
        {
            bool inserted = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("insert into gasto (descripcion,precio,cantidad,total,fecha,fkconcepto,fkcobranza) " +
                    "values(@desc,@precio,@cant,@total,@f,@fkcon,@fkcob)", conn))
                {
                    cmd.Parameters.AddWithValue("@desc", gastoLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", gastoLocal.Precio);
                    cmd.Parameters.AddWithValue("@cant", gastoLocal.Cantidad);
                    cmd.Parameters.AddWithValue("@total", gastoLocal.Total);
                    cmd.Parameters.AddWithValue("@f", gastoLocal.Fecha);
                    cmd.Parameters.AddWithValue("@fkcon", gastoLocal.FkConcepto);
                    cmd.Parameters.AddWithValue("@fkcob", gastoLocal.FkCobranza);
                    var result = cmd.ExecuteNonQuery();
                    inserted = result > 0 ? true : false;
                }
                return inserted;
            }
        }
        public decimal Suma
        {
            get
            {
                return suma;
            }
        }
        public GastoLocal Get(int id)
        {
            GastoLocal gasto = null;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from gasto where id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", id);
                    var rd = cmd.ExecuteReader(); 
                    if (rd.Read())
                    {
                        gasto = new GastoLocal();
                        gasto.Id = int.Parse(rd["id"].ToString());
                        gasto.Descripcion = rd["descripcion"] as string;
                        gasto.Precio = decimal.Parse(rd["precio"].ToString());
                        gasto.Cantidad = int.Parse(rd["cantidad"].ToString());
                        gasto.Total = decimal.Parse(rd["total"].ToString());
                        gasto.Fecha = rd["fecha"] as string;
                        gasto.FkConcepto = int.Parse(rd["fkconcepto"].ToString());
                        gasto.Concepto = conceptoRepo.Get(gasto.FkConcepto).Descripcion;
                        gasto.FkCobranza = int.Parse(rd["fkcobranza"].ToString());
                        gasto.Folio = cobranzaRepo.Get(gasto.FkCobranza).Folio;
                    }
                }
                return gasto;

            }
        }

        public decimal getGastoTotalByCobranza(int idCobranza)
        {
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT SUM(total) FROM gasto WHERE fkcobranza = @idcobranza", conn))
                {
                    cmd.Parameters.AddWithValue("@idcobranza", idCobranza);
                    var result = cmd.ExecuteScalar();
                    if (result != null && !(result is DBNull))
                    {
                        return decimal.Parse(result.ToString());
                    }
                    return 0;
                }
            }
        }


        public List<GastoLocal> GetAll()
        {
            List<GastoLocal> gastos = new List<GastoLocal>();
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from gasto order by id desc", conn))
                {
                    var rd = cmd.ExecuteReader();
                    suma = 0;
                    while (rd.Read())
                    {
                        GastoLocal gasto = new GastoLocal();
                        gasto.Id = int.Parse(rd["id"].ToString());
                        gasto.Descripcion = rd["descripcion"] as string;
                        gasto.Precio = decimal.Parse(rd["precio"].ToString());
                        gasto.Cantidad = int.Parse(rd["cantidad"].ToString());
                        gasto.Total = decimal.Parse(rd["total"].ToString());
                        gasto.Fecha = rd["fecha"] as string;
                        gasto.FkConcepto = int.Parse(rd["fkconcepto"].ToString());
                        gasto.Concepto = conceptoRepo.Get(gasto.FkConcepto).Descripcion; 
                        gasto.FkCobranza = int.Parse(rd["fkcobranza"].ToString());
                        gasto.Folio = cobranzaRepo.Get(gasto.FkCobranza).Folio;
                        suma += gasto.Total;
                        gastos.Add(gasto);
                    }
                }
                return gastos;

            }
        }

        public bool Update(GastoLocal gastoLocal)
        {
            bool updated = false;
            using (var conn = new SQLiteConnection(connection))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("update gasto set descripcion=@desc,precio=@precio,cantidad=@cant,total=@total,fecha=@f,fkconcepto=@fkcon,fkcobranza=@fkcob" +
                    "WHERE id=@id", conn))
                {
                    cmd.Parameters.AddWithValue("@id", gastoLocal.Id);
                    cmd.Parameters.AddWithValue("@desc", gastoLocal.Descripcion);
                    cmd.Parameters.AddWithValue("@precio", gastoLocal.Precio);
                    cmd.Parameters.AddWithValue("@cant", gastoLocal.Cantidad);
                    cmd.Parameters.AddWithValue("@total", gastoLocal.Total);
                    cmd.Parameters.AddWithValue("@f", gastoLocal.Fecha);
                    cmd.Parameters.AddWithValue("@fkcon", gastoLocal.FkConcepto);
                    cmd.Parameters.AddWithValue("@fkcob", gastoLocal.FkCobranza);
                    var result = cmd.ExecuteNonQuery();
                    updated = result > 0 ? true : false;
                }
                return updated;
            }
        }
    }
}
