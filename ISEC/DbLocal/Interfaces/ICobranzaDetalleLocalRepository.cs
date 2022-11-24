using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Local;
namespace ISEC.DbLocal.Interfaces
{
    public interface ICobranzaDetalleLocalRepository
    {
        List<CobranzaDetalleLocal> GetAll();
        CobranzaDetalleLocal Get (int id);
        string Last();
        int GetWeekForPay(int alumnoId);
        bool Add(CobranzaDetalleLocal pago);
        bool Update(CobranzaDetalleLocal pago);
        PagoLocalViewModel GetLastPago();
    }
}
