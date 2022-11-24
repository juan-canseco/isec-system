 

namespace DataAccess.Local
{ 
    public class CobranzaDetalleLocal
    {
        public int Id { get; set; }
        public string Folio { get; set; }
        public int FkCobranza { get; set; }
        public int FkAlumno { get; set; }
        public string Fecha { get; set; }
        public int EsIngreso { get; set; }
        public int EsEgreso { get; set; }
        public decimal Cantidad { get; set; }
        public int Semanas { get; set; }
        public string Clave { get; set; }
        public string Username { get; set; }
        public decimal Resto { get; set; }
        public int Week1 { get; set; }
        public int Week2 { get; set; }
        public string DtWeek1 { get; set; }
        public string DtWeek2 { get; set; }


    }
}
