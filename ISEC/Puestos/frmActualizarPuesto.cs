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
using DataAccess.Local;
namespace ISEC.Puestos
{
    public partial class frmActualizarPuesto : Form
    {
        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository();
        PuestoLocal puesto = null;
        public Action RefreshAll { get; set; }
        public frmActualizarPuesto(PuestoLocal _puesto)
        {
            InitializeComponent();
            if (_puesto != null)
            {
                puesto = _puesto;
                txtDescripcion.Text = puesto.Descripcion;
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Es necesario una descripción para registrar un nuevo puesto", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool inserted = puestoLocalRepository.Update(new PuestoLocal()
            {
                Id = puesto.Id,
                Descripcion = txtDescripcion.Text
            });
            if (inserted)
            {
                MessageBox.Show($"Se ha actualizado exitosamente el puesto {txtDescripcion.Text}", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Clear();
                txtDescripcion.Focus();
                RefreshAll();
                return;
            }
            else
            {
                MessageBox.Show($"No se logro actualizar puesto {txtDescripcion.Text}", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDescripcion.Focus();
                return;
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
