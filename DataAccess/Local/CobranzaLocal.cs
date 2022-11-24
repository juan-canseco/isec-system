 

namespace DataAccess.Local
{
    public class CobranzaLocal
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public string  Username { get; set; }
        public decimal FondoInicial { get; set; }
        public string FechaInicial { get; set; }
        public string FechaFinal { get; set; }
        public decimal Sobrante { get; set; }
        public decimal Faltante { get; set; }
        public decimal Ingreso { get; set; }
        public decimal Egreso { get; set; }
        public int Estado { get; set; }
        public decimal SaldoCaja { get; set; }

    }
}
