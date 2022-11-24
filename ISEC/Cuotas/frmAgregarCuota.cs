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

namespace ISEC.Cuotas
{
    public partial class frmAgregarCuota : Form
    {
        CuotaLocalRepository cuotaLocalRepository = new CuotaLocalRepository();
        public Action RefreshAll { get; set; }
        public frmAgregarCuota()
        {
            InitializeComponent();
        } 
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(txtDescripcion.Text))
                {
                    MessageBox.Show($"Ingrese una descripción", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtTipo.Text))
                {
                    MessageBox.Show($"Ingrese un tipo", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                if (string.IsNullOrEmpty(txtColegiatura.Text))
                {
                    MessageBox.Show($"Ingrese una colegiatura", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                CuotaLocal cuota = new CuotaLocal()
                {
                    Tipo = txtTipo.Text,
                    Descripcion = txtDescripcion.Text,
                    Colegiatura = decimal.Parse(txtColegiatura.Text)
                };
                bool update = cuotaLocalRepository.Add(cuota);
                if (update)
                {
                    MessageBox.Show($"Se agrego cuota exitosamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    RefreshAll();
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error:{ex.Message}", "ERROR!", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}
