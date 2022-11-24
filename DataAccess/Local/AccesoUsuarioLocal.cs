 

namespace DataAccess.Local
{
    public class AccesoUsuarioLocal
    {
        public int Id { get; set; }
        public int RolId { get; set; }
        public string Rol { get; set; }
        public int ModuloId { get; set; }
        public string Modulo { get; set; }
        public bool Acceso { get; set; }
        public bool Lectura { get; set; }
        public bool Escritura { get; set; }

        public int Sync { get; set; }
    }
}
