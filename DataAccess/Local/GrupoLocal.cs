using System; 
namespace DataAccess.Local
{
    public class GrupoLocal
    {
        public int Id { get; set; }
        public int Numero { get; set; }
        public string Letra { get; set; }
        public int FkHorario { get; set; }
        public int EsActivo { get; set; }

        public string Grupo
        {
            get
            {
                return String.Format("{0} {1}", Numero, Letra);
            }
        }
        public HorarioLocal HorarioObj { get; set; }
        public string Horario
        {
            get
            {
                if (HorarioObj != null)
                {
                    return HorarioObj.Descripcion;
                }
                else
                {
                    return "No asignado";
                }
            }
        }
        public string Inicio
        {
            get
            {
                if (HorarioObj != null)
                {
                    return HorarioObj.Inicio;
                }
                else
                {
                    return "00:00";
                }
            }
        }
        public string Fin
        {
            get
            {
                if (HorarioObj != null)
                {
                    return HorarioObj.Fin;
                }
                else
                {
                    return "00:00";
                }
            }
        }
        public bool Activo
        {
            get
            {
                if (EsActivo > 0)
                    return true;
                return false;

            }
        }

    }
}
