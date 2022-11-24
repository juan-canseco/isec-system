 namespace DataAccess.Local
{
    public  class PagoLocalViewModel
    {
        public string Folio { get; set; }
        public string Fecha { get; set; }
        public string Username { get; set; }
        public string Descripcion
        {
            get
            {
                return $"{Cuota} ({Clave})";
            }
        }
        public string Pago
        {
            get
            {
                return Cantidad.ToString("C");
            }
        }
        public string Credencial { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public decimal Cantidad { get; set; }
        public decimal Resto { get; set; }
        public string Clave { get; set; }
        public string Cuota { get; set; } 
        public int FkAlumno { get; set; }
        public string Grupo { get; set; }
        public string Horario { get; set; }
        public string Carrera { get; set; }
        public string Cuotaa { get; set; }
        public string Plan { get; set; }
        public int Week1 { get; set; }
        public int Week2 { get; set; }
        public string DtWeek1 { get; set; }
        public string DtWeek2 { get; set; }
     

    }
}
