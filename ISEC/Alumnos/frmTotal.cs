using DataAccess.Local;
using ISEC.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ISEC
{
    public partial class frmTotal : Form
    {
        public Action<List<ClaveViewModel>> RefreshData {get;set;}
        List<ClaveViewModel> claves = new List<ClaveViewModel>();
        ClaveViewModel clave = new ClaveViewModel();
        public frmTotal()
        {
            InitializeComponent();
       
        }
        public frmTotal(ClaveLocal _clave, ClaveViewModel update)
        {
            InitializeComponent();
            if (update != null)
            {
                txtImporte.Text = update.Importe.ToString();
                txtAbono.Text = update.Abono.ToString();
                clave.Clave = update.Clave;
                clave.Cuota = update.Cuota;
            }
            txtImporte.Focus(); 
            if (_clave != null)
            {
                clave.Clave = _clave.Clave;
                clave.Cuota = _clave.Cuota;

                if (chAbono.Checked)
                {
                    lblAbono.Visible = true;
                    txtAbono.Visible = true;
                }
                else
                {
                    lblAbono.Visible = false;
                    txtAbono.Visible = false;
                }
            }
          
        } 
        private void chAbono_CheckedChanged(object sender, EventArgs e)
        {
            var c = sender as CheckBox;
            if (c.Checked)
            {
                if (!string.IsNullOrEmpty(txtImporte.Text))
                {
                    lblAbono.Visible = true;
                    txtAbono.Visible = true;
                    txtAbono.Focus();
                }
                else
                {
                    MessageBox.Show("Ingrese un importe por favor", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtImporte.Focus(); 
                }
            }
            else
            {
                lblAbono.Visible = false;
                txtAbono.Visible = false;
            }
        }
        private void EventLoad(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!chAbono.Checked)
                {
                    if (!string.IsNullOrEmpty(txtImporte.Text))
                    {
                        clave.Importe = decimal.Parse(txtImporte.Text);
                    }
                    else
                    {
                        MessageBox.Show("Ingrese un importe por favor", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        txtImporte.Focus();
                        return;
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(txtImporte.Text) && !string.IsNullOrEmpty(txtAbono.Text))
                    {
                        clave.Importe = decimal.Parse(txtImporte.Text);
                        clave.Abono = decimal.Parse(txtAbono.Text);


                    }
                    else
                    {
                        MessageBox.Show("Ingrese un importe y abono por favor", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        return;
                    }

                }
                this.Close();
                claves.Add(clave);
                RefreshData(claves);
            }
        }

        private void txtImporte_KeyDown(object sender, KeyEventArgs e)
        {
            EventLoad(sender,e);
        } 
        private void txtAbono_KeyDown(object sender, KeyEventArgs e)
        {
            EventLoad(sender, e);
        }
    }
}
