using DataAccess.Local;
using System.Data.SQLite;
using System.Data;
using ISEC.DbLocal.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISEC.DbLocal.Repositorios
{
    public class ArqueoLocalRepository : IArqueoLocalRepository
    {
        private readonly string connectionString; 

        public ArqueoLocalRepository()
        {
            connectionString = ConnectionLocal.ConecctionString;
        }

        public bool Add(ArqueoLocal arqueoLocal)
        {
            using (var connection = new SQLiteConnection(connectionString))
            {
               
                var sb = new StringBuilder();

                sb.AppendLine("INSERT INTO arqueo ");
                sb.AppendLine("(");
                sb.AppendLine("folio, fkcobranza, fkusuario, nm10c, nm20c, nm50c, nm1p,");
                sb.AppendLine("nm2p, nm5p, nm10p, nb20p, nb50p, nb100p, nb200p, nb500p, nb1000p,");
                sb.AppendLine("retiroefectivo, fondoendcaja, subtotalefectivo, totalefectivo, fechacreacion");
                sb.AppendLine(") ");
                sb.AppendLine("VALUES ");
                sb.AppendLine("(");
                sb.AppendLine("@folio, @fkcobranza, @fkusuario, @nm10c, @nm20c, @nm50c, @nm1p,");
                sb.AppendLine("@nm2p, @nm5p, @nm10p, @nb20p, @nb50p, @nb100p, @nb200p, @nb500p, @nb1000p,");
                sb.AppendLine("@retiroefectivo, @fondoencaja, @subtotalefectivo, @totalefectivo, @fechacreacion");
                sb.AppendLine(")");

                connection.Open();

                using (var command = new SQLiteCommand(sb.ToString(), connection))
                {
                    command.Parameters.AddWithValue("@folio", arqueoLocal.Folio);
                    command.Parameters.AddWithValue("@fkcobranza", arqueoLocal.FkCobranza);
                    command.Parameters.AddWithValue("@fkusuario", arqueoLocal.FkUsuario);
                    command.Parameters.AddWithValue("@nm10c", arqueoLocal.NM10C);
                    command.Parameters.AddWithValue("@nm20c", arqueoLocal.NM20C);
                    command.Parameters.AddWithValue("@nm50c", arqueoLocal.NM50C);
                    command.Parameters.AddWithValue("@nm1p", arqueoLocal.NM1P);
                    command.Parameters.AddWithValue("@nm2p", arqueoLocal.NM2P);
                    command.Parameters.AddWithValue("@nm5p", arqueoLocal.NM5P);
                    command.Parameters.AddWithValue("@nm10p", arqueoLocal.NM10P);
                    command.Parameters.AddWithValue("@nb20p", arqueoLocal.NB20P);
                    command.Parameters.AddWithValue("@nb50p", arqueoLocal.NB50P);
                    command.Parameters.AddWithValue("@nb100p", arqueoLocal.NB100P);
                    command.Parameters.AddWithValue("@nb200p", arqueoLocal.NB200P);
                    command.Parameters.AddWithValue("@nb500p", arqueoLocal.NB500P);
                    command.Parameters.AddWithValue("@nb1000p", arqueoLocal.NB1000P);
                    command.Parameters.AddWithValue("@retiroefectivo", arqueoLocal.RetiroEfectivo);
                    command.Parameters.AddWithValue("@fondoencaja", arqueoLocal.FondoEnCaja);
                    command.Parameters.AddWithValue("@subtotalefectivo", arqueoLocal.SubtotalEfectivo);
                    command.Parameters.AddWithValue("@totalefectivo", arqueoLocal.TotalEfectivo);
                    command.Parameters.AddWithValue("@fechacreacion", DateTimeToString(arqueoLocal.FechaCreacion));

                    var result = command.ExecuteNonQuery();
                    return result > 0;
                }
            }
        }

        public bool Desactive(ArqueoLocal arqueoLocal)
        {
            throw new NotImplementedException();
        }

        public int GenerateFolio()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT folio FROM arqueo ORDER BY folio desc limit 1", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        return int.Parse(rd[0].ToString()) + 1;
                    }
                }
                return 0 + 1;
            }
        }



        private string DateTimeToString(DateTime time)
        {
            string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return time.ToString(DateTimeFormat);
        }


        private DateTime StringToDateTime(string time)
        {
            string DateTimeFormat = "yyyy-MM-dd HH:mm:ss";
            return DateTime.ParseExact(time, DateTimeFormat, null);
        }


        private ArqueoLocal BuildArqueoFromReader(SQLiteDataReader reader)
        {
            ArqueoLocal arqueoLocal = new ArqueoLocal();
            arqueoLocal.Id = int.Parse(reader["id"].ToString());
            arqueoLocal.Folio = int.Parse(reader["folio"].ToString());
            arqueoLocal.FkCobranza = int.Parse(reader["fkcobranza"].ToString());
            arqueoLocal.FkUsuario = int.Parse(reader["fkusuario"].ToString());
            arqueoLocal.NM10C = int.Parse(reader["nm10c"].ToString());
            arqueoLocal.NM20C = int.Parse(reader["nm20c"].ToString());
            arqueoLocal.NM50C = int.Parse(reader["nm50c"].ToString());
            arqueoLocal.NM1P = int.Parse(reader["nm1p"].ToString());
            arqueoLocal.NM2P = int.Parse(reader["nm2p"].ToString());
            arqueoLocal.NM5P = int.Parse(reader["nm5p"].ToString());
            arqueoLocal.NM10P = int.Parse(reader["nm10p"].ToString());
            arqueoLocal.NB20P = int.Parse(reader["nb20p"].ToString());
            arqueoLocal.NB50P = int.Parse(reader["nb50p"].ToString());
            arqueoLocal.NB100P = int.Parse(reader["nb100p"].ToString());
            arqueoLocal.NB200P = int.Parse(reader["nb200p"].ToString());
            arqueoLocal.NB500P = int.Parse(reader["nb500p"].ToString());
            arqueoLocal.NB1000P = int.Parse(reader["nb1000p"].ToString());
            arqueoLocal.RetiroEfectivo = decimal.Parse(reader["retiroefectivo"].ToString());
            arqueoLocal.FondoEnCaja = decimal.Parse(reader["fondoendcaja"].ToString());
            arqueoLocal.SubtotalEfectivo = decimal.Parse(reader["subtotalefectivo"].ToString());
            arqueoLocal.TotalEfectivo = decimal.Parse(reader["totalefectivo"].ToString());
            arqueoLocal.FechaCreacion = StringToDateTime(reader["fechacreacion"].ToString());
            return arqueoLocal;
        }

        public ArqueoLocal Get(int id)
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("select * from arqueo where id = @id", conn))
                {
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    if (rd.Read())
                    {
                        return BuildArqueoFromReader(rd);
                    }
                    return null;
                }
            }
        }

        public List<ArqueoLocal> GetAll()
        {
            using (var conn = new SQLiteConnection(connectionString))
            {
                conn.Open();
                using (var cmd = new SQLiteCommand("SELECT * FROM arqueo", conn))
                {
                    List<ArqueoLocal> arqueos = new List<ArqueoLocal>();
                    cmd.CommandType = CommandType.Text;
                    var rd = cmd.ExecuteReader();
                    while (rd.Read())
                    {
                        var arqueo =  BuildArqueoFromReader(rd);
                        arqueos.Add(arqueo);
                    }
                    return arqueos;
                }
            }
        }

        public bool Update(ArqueoLocal arqueoLocal)
        {
            throw new NotImplementedException();
        }
    }
}
