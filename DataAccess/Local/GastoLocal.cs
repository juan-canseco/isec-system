namespace DataAccess.Local
{
    public class GastoLocal
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Total { get; set; }
        public string Fecha { get; set; }
        public int FkConcepto { get; set; }
        public string Concepto { get; set; }
        public int FkCobranza { get; set; }
        public string Folio { get; set; }
    }

    public class RolLocal2
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public bool Sync { get; set; }
        public string LastDate { get; set; }
    }
}
