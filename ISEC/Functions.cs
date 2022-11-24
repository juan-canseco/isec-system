using DataAccess.Local;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Modelos
{
    public class Functions
    {

        public static  void Sync<T>(frmMenu menu,List<T> data,List<Action> methods)
        {
            if (NetWork.IsConnected)
            {
                if (data != null && data.Count > 0)
                {
                    menu.lblTituloProgreso.Text = "Sincronizando...";

                    menu.lblTituloProgreso.ForeColor = SystemColors.ActiveBorder;
                    if (!menu.bgGlobal.IsBusy)
                    {
                        menu.Method =methods[0];
                        menu.RefreshAll =methods[1];
                        menu.bgGlobal.RunWorkerAsync();
                    }
                } 
            }
            else
            {
                menu.lblTituloProgreso.ForeColor = Color.Red;
                menu.lblTituloProgreso.Text = "No hay conexion a internet";
            }
            methods[1]();
        }
        public static void SetRoles(UsuarioViewModel usVm,Button btn,DataGridView gv)
        {
            if (usVm != null)
            { 
                if (usVm.Acceso > 0)
                {
                    if (usVm.Escritura == 0)
                    {
                        btn.Enabled = false;
                        gv.Enabled = false;
                    }
                    else
                    {
                        btn.Enabled = true;
                        gv.Enabled = true;
                    }
                }
            }
        }

        public static int Week(DateTime time)
        {
            // Seriously cheat.  If its Monday, Tuesday or Wednesday, then it'll 
            // be the same week# as whatever Thursday, Friday or Saturday are,
            // and we always get those right
            DayOfWeek day = CultureInfo.InvariantCulture.Calendar.GetDayOfWeek(time);
            if (day >= DayOfWeek.Monday && day <= DayOfWeek.Wednesday)
            {
                time = time.AddDays(3);
            }

            // Return the week of our adjusted day
            return CultureInfo.InvariantCulture.Calendar.GetWeekOfYear(time, CalendarWeekRule.FirstFourDayWeek, DayOfWeek.Monday);
        }
    }
}
