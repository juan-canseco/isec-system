using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Local
{
    public class ArqueoLocal
    {
        public int Id { get; set; }
        public int Folio { get; set; }
        public int FkCobranza { get; set; }
        public int FkUsuario { get; set; }
        public int NM10C { get; set; }
        public int NM20C { get; set; }
        public int NM50C { get; set; }
        public int NM1P { get; set; }
        public int NM2P { get; set; }
        public int NM5P { get; set; }
        public int NM10P { get; set; }
        public int NB20P { get; set; }
        public int NB50P { get; set; }
        public int NB100P { get; set; }
        public int NB200P { get; set; }
        public int NB500P { get; set; }
        public int NB1000P { get; set; }
        public decimal RetiroEfectivo { get; set; }
        public decimal FondoEnCaja { get; set; }
        public decimal SubtotalEfectivo { get; set; }
        public decimal TotalEfectivo { get; set; }
        public DateTime FechaCreacion { get; set; }
    }
}
