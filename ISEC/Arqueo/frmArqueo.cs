using ISEC.DbLocal.Repositorios;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC.Arqueo
{
    public partial class frmArqueo : Form
    {
        GastoLocalRepository gastoRepo = new GastoLocalRepository(); 
        public frmArqueo()
        {
            InitializeComponent();
            //lblFecha.Text = DateTime.Now.ToLongDateString();
            ReloadGastos(); 
        }
        public void ReloadGastos()
        {
            gvGastos.DataSource = null;
            gvGastos.DataSource = gastoRepo.GetAll();
            lblSubtotal.Text = gastoRepo.Suma.ToString("n2");
        }
        public void Sum()
        {
            var sum = (decimal)0.10 * txtCantidadP10.Value
                + (decimal)0.20 * txtCantidadP20.Value
                + (decimal)0.50 * txtCantidadP50.Value
                + (decimal)1.00 * txtCantidad1Peso.Value
                + (decimal)2.00 * txtCantidad2Pesos.Value
                + (decimal)5.00 * txtCantidad5Pesos.Value
                + (decimal)10.00 * txtCantidad10Pesos.Value
                + (decimal)20.00 * txtBillete20.Value
                + (decimal)50.00 * txtBillete50.Value
                + (decimal)100.00 * txtBillete100.Value
                + (decimal)200.00 * txtBillete200.Value
                + (decimal)500.00 * txtBillete500.Value
                + (decimal)1000.00 * txtBillete1000.Value;
            txtTotalEfectivo.Text = sum.ToString("C");
        }
        private void txtCantidadP10_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidadP10.Value>0)
            {
                txtImporteMP1.Text = ((decimal)0.10 * txtCantidadP10.Value).ToString("C");
                Sum();

            }
            else
            {
                txtImporteMP1.Text = "";
            } 
        }

        private void txtCantidadP20_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidadP20.Value > 0)
            {
                txtImporteMP2.Text = ((decimal)0.20 * txtCantidadP20.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteMP2.Text = "";
            }
        }

        private void txtCantidadP50_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidadP50.Value > 0)
            {
                txtImporteMP5.Text = ((decimal)0.50 * txtCantidadP50.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteMP5.Text = "";
            }
        }

        private void txtCantidad1Peso_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidad1Peso.Value > 0)
            {
                txtImporteM1.Text = ((decimal)1.00 * txtCantidad1Peso.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteM1.Text = "";
            }
        }

        private void txtCantidad2Pesos_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidad2Pesos.Value > 0)
            {
                txtImporteM2.Text = ((decimal)1.00 * txtCantidad2Pesos.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteM2.Text = "";
            }
        }

        private void txtCantidad5Pesos_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidad5Pesos.Value > 0)
            {
                txtImporteM5.Text = ((decimal)5.00 * txtCantidad5Pesos.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteM5.Text = "";
            }
        }

        private void txtCantidad10Pesos_ValueChanged(object sender, EventArgs e)
        {
            if (txtCantidad10Pesos.Value > 0)
            {
                txtImporteM10.Text = ((decimal)10.00 * txtCantidad10Pesos.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteM10.Text = "";
            }
        }

        private void txtBillete20_ValueChanged(object sender, EventArgs e)
        {

            if (txtBillete20.Value > 0)
            {
                txtImporteB20.Text = ((decimal)20.00 * txtBillete20.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB20.Text = "";
            }
        }

        private void txtBillete50_ValueChanged(object sender, EventArgs e)
        {

            if (txtBillete50.Value > 0)
            {
                txtImporteB50.Text = ((decimal)50.00 * txtBillete50.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB50.Text = "";
            }
        }

        private void txtBillete100_ValueChanged(object sender, EventArgs e)
        {

            if (txtBillete100.Value > 0)
            {
                txtImporteB100.Text = ((decimal)100.00 * txtBillete100.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB100.Text = "";
            }
        }

        private void txtBillete200_ValueChanged(object sender, EventArgs e)
        {
            if (txtBillete200.Value > 0)
            {
                txtImporteB200.Text = ((decimal)200.00 * txtBillete200.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB200.Text = "";
            }
        }

        private void txtBillete500_ValueChanged(object sender, EventArgs e)
        {
            if (txtBillete500.Value > 0)
            {
                txtImporteB500.Text = ((decimal)500.00 * txtBillete500.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB500.Text = "";
            }
        }

        private void txtBillete1000_ValueChanged(object sender, EventArgs e)
        {
            if (txtBillete1000.Value > 0)
            {
                txtImporteB1000.Text = ((decimal)1000.00 * txtBillete1000.Value).ToString("C");
                Sum();
            }
            else
            {
                txtImporteB1000.Text = "";
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
