using System; 

namespace DataAccess.Local
{
    public class HorarioLocal
    {
        public int Id { get; set; }
        public string Descripcion { get; set; }
        public int Lunes { get; set; }
        public int Martes { get; set; }
        public int Miercoles { get; set; }
        public int Jueves { get; set; }
        public int Viernes { get; set; }
        public int Sabado { get; set; }
        public int Domingo { get; set; }
        public string HoraInicial { get; set; }
        public string Inicio
        {
            get
            {
                if (!string.IsNullOrEmpty(HoraInicial))
                {
                    return DateTime.Parse(HoraInicial).ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string Fin
        {
            get
            {
                if (!string.IsNullOrEmpty(HoraFinal))
                {
                    return DateTime.Parse(HoraFinal).ToShortTimeString();
                }
                else
                {
                    return "";
                }
            }
        }
        public string HoraFinal { get; set; }
        public int Activo { get; set; }
        public bool Active
        {
            get
            {
                if (Activo > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
