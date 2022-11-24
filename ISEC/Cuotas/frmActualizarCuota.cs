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
    public partial class frmActualizarCuota : Form
    {
        CuotaLocalRepository cuotaLocalRepository = new CuotaLocalRepository();
        public Action RefreshAll { get; set; }
        CuotaLocal cuota = null;
        public frmActualizarCuota(CuotaLocal _cuota)
        {
            InitializeComponent();
            cuota = _cuota;
            txtTipo.Text = cuota.Tipo;
            txtDescripcion.Text = cuota.Descripcion;
            txtColegiatura.Text = cuota.Colegiatura.ToString();
            chActivo.Checked = cuota.Activa;
        }
        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        } 
        private void btnActualizar_Click(object sender, EventArgs e)
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
                    Id = this.cuota.Id,
                    Tipo = txtTipo.Text,
                    Descripcion = txtDescripcion.Text,
                    Colegiatura = decimal.Parse(txtColegiatura.Text),
                    Activa = chActivo.Checked
                };
                bool update = cuotaLocalRepository.Update(cuota);
                if (update)
                {
                    MessageBox.Show($"Se modificó cuota exitosamente!", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
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
