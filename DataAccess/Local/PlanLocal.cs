 

namespace DataAccess.Local
{
    public class PlanLocal
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public string Fecha { get; set; }
        public int EsActivo { get; set; }
        public bool Activo
        {
            get
            {
                return EsActivo > 0 ? true : false;
            }
        }
    }
}
