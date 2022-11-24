 

namespace DataAccess.Local
{
    public class AlumnoLocal
    {
        public int Id { get; set; } 
        public string Credencial { get; set; }
        public string Nombre { get; set; }
        public string Direccion { get; set; }
        public string Telefono { get; set; }
        public int CarreraId { get; set; }
        public int CuotaId { get; set; }
        public string FechaIngreso { get; set; }
        public string Consejero { get; set; }
        public int Semana { get; set; }
        public int EsActivo { get; set; }
        public string Usuario { get; set; }
        public int Baja { get; set; }
        public string FechaBaja { get; set; }
        public int Reingreso { get; set; }
        public string FechaReingreso { get; set; }
        public int FkGrupo { get; set; }

        public int FkPlan { get; set; }
        public bool Activo
        {
            get
            {
                return EsActivo > 0 ? true : false;
            }
        }
    }
}
