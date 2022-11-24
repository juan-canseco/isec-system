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
    public partial class frmAgregarPuesto : Form
    {
        PuestoLocalRepository puestoLocalRepository = new PuestoLocalRepository(); 
        public Action RefreshAll { get; set; }
        public frmAgregarPuesto()
        {
            InitializeComponent();
        } 
        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtDescripcion.Text))
            {
                MessageBox.Show("Es necesario una descripción para registrar un nuevo puesto", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            bool inserted = puestoLocalRepository.Add(new PuestoLocal()
            {
                Descripcion= txtDescripcion.Text
            });
            if (inserted)
            {
                MessageBox.Show($"Se ha registrado exitosamente el puesto {txtDescripcion.Text}", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDescripcion.Clear();
                txtDescripcion.Focus();
                RefreshAll();
                return;
            }
            else
            {
                MessageBox.Show($"No se logro registrar puesto {txtDescripcion.Text}", "AVISO!", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
