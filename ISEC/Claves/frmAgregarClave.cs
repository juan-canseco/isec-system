using DataAccess.Local;
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

namespace ISEC.Claves
{
    public partial class frmAgregarClave : Form
    {
        ClaveLocalRepository claveLocalRepository = new ClaveLocalRepository();
        public Action RefreshAll { get; set; }
        public frmAgregarClave()
        {
            InitializeComponent();
        }

        private void frmAgregarClave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Close();
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtClave.Text))
                {
                    MessageBox.Show("Es necesario ingresar una clave", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtCuota.Text))
                {
                    MessageBox.Show("Es necesario ingresar una cuota", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtPrecio.Text))
                {
                    MessageBox.Show("Es necesario ingresar un precio", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                ClaveLocal clave = new ClaveLocal()
                {
                    Clave = txtClave.Text,
                    Cuota = txtCuota.Text,
                    Precio = decimal.Parse(txtPrecio.Text)
                };
                bool inserted = claveLocalRepository.Add(clave);
                if (inserted)
                {
                    MessageBox.Show("Se ha ingresado clave exitosamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }

        }
    }
}
