using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls; 
namespace ISEC.Utilidades
{
    public class Utilities
    {

        public static DateTime SafeParse(string strDate)
        {
            var cleanDate = strDate.Replace("a. m.", "AM").Replace("p. m.", "PM");
            return DateTime.ParseExact(cleanDate, "d/MM/yyyy h:mm:ss tt", CultureInfo.InvariantCulture);
        }

        public static void ResetAllControls(System.Windows.Forms.Form form)
        {
            foreach (Control control in form.Controls)
            {
                if (control is TextBox)
                {
                    TextBox textBox = (TextBox)control;
                    textBox.Text = null;
                }

                if (control is ComboBox)
                {
                    ComboBox comboBox = (ComboBox)control;
                    if (comboBox.Items.Count > 0)
                        comboBox.SelectedIndex = 0;
                }

                if (control is CheckBox)
                {
                    CheckBox checkBox = (CheckBox)control;
                    checkBox.IsChecked = false;
                }
                 
            }
        }
    }
}
